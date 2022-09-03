using System.Diagnostics;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using XProxy.Domain;
using XProxy.Interfaces;
using XProxy.Services;
using XProxy.Web.Models;

namespace XProxy.Web.Controllers;

public class ExchangeController : Controller
{
    private readonly ILogger<ExchangeController> _logger;
    private readonly IExchangeServiceFactory _exchangeServiceFactory;
    private readonly IXProxyOptions _options;
    private readonly ISettingsService _settingsService;
    private readonly IUserSettingsStorage _userSettingsStorage;
    private readonly IRecurringJobManager _recurringJobManager;

    public ExchangeController(
        IExchangeServiceFactory exchangeServiceFactory,
        IXProxyOptions xProxyOptions,
        ISettingsService settingsService,
        IUserSettingsStorage userSettingsStorage,
        IRecurringJobManager recurringJobManager,
        ILogger<ExchangeController> logger)
    {
        _logger = logger;
        _options = xProxyOptions;
        _exchangeServiceFactory = exchangeServiceFactory;
        _settingsService = settingsService;
        _userSettingsStorage = userSettingsStorage;
        _recurringJobManager = recurringJobManager;
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public async Task<IActionResult> CheckAndPull(long fromId, long toId)
    {
        try
        {
            var exchangeService = await _exchangeServiceFactory.CreateDefaultAsync();
            return Json(await exchangeService.AV100CheckAndLoad(fromId, toId, HttpContext.RequestAborted));
        }
        catch (Exception ex)
        {
            _logger.Log(LogLevel.Debug, ex, "Exchange/CheckAndPull exception");
            return Json(new ExchangeResult { Result = false, Message = ex.Message });
        }
    }

    public async Task<JsonResult> LoadRetro()
    {
        try
        {
            var exchangeService = await _exchangeServiceFactory.CreateDefaultAsync();
            return Json(await exchangeService.AV100LoadRetro(HttpContext.RequestAborted));
        }
        catch (Exception ex)
        {
            _logger.Log(LogLevel.Debug, ex, "Exchange/LoadRetro exception");
            return Json(new ExchangeResult { Result = false, Message = ex.Message });
        }
    }
    
    // public async Task<IActionResult> StartHangfire()
    // {
    //     var exchangeService = await _exchangeServiceFactory.CreateDefaultAsync();
    //     _recurringJobManager.AddOrUpdate(
    //         "CheckAndPull",
    //         () => exchangeService.AV100CheckAndLoad(HttpContext.RequestAborted),
    //         Cron.Minutely);
    //
    //     return Redirect("/");
    // }
    
    public IActionResult StartHangfire()
    {
        _recurringJobManager.AddOrUpdate(
            "CheckAndPull",
            () => RunJob(), // use public local method only (singularity of Expression<Func<Task>> parameter)
            Cron.Minutely);
    
        return Redirect("/");
    }
    
    public async Task RunJob()
    {
        var token = CancellationToken.None; // HttpContext is null because RunJob will call out of HTTP scope
        var service = await _exchangeServiceFactory.CreateDefaultAsync(token);
        await service.AV100CheckAndLoad(token);
    }
}