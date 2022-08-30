using System.Diagnostics;
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

    public ExchangeController(
        IExchangeServiceFactory exchangeServiceFactory,
        IXProxyOptions xProxyOptions,
        ISettingsService settingsService,
        IUserSettingsStorage userSettingsStorage,
        ILogger<ExchangeController> logger)
    {
        _logger = logger;
        _options = xProxyOptions;
        _exchangeServiceFactory = exchangeServiceFactory;
        _settingsService = settingsService;
        _userSettingsStorage = userSettingsStorage;
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
}