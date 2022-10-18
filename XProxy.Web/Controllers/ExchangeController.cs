using System.Diagnostics;
using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;
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
    private readonly ITelegramBotService _telegramBotService;

    public ExchangeController(
        IExchangeServiceFactory exchangeServiceFactory,
        IXProxyOptions xProxyOptions,
        ISettingsService settingsService,
        IUserSettingsStorage userSettingsStorage,
        IRecurringJobManager recurringJobManager,
        ITelegramBotService telegramBotService,
        ILogger<ExchangeController> logger)
    {
        _logger = logger;
        _options = xProxyOptions;
        _exchangeServiceFactory = exchangeServiceFactory;
        _settingsService = settingsService;
        _userSettingsStorage = userSettingsStorage;
        _recurringJobManager = recurringJobManager;
        _telegramBotService = telegramBotService;
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> CheckAndPull(long fromId, long toId)
    {
        try
        {
            var exchangeService = await _exchangeServiceFactory.CreateDefaultAsync();
            var result = await exchangeService.AV100CheckAndLoad(fromId, toId, HttpContext.RequestAborted);
            if (result.Result)
            {
                XProxyLogger.Log($"{nameof(ExchangeController)}/{nameof(CheckAndPull)}", $"Inserted: {result.SucceessfulCount}",
                    _telegramBotService, HttpContext.RequestAborted);
            }
            return Json(result);
        }
        catch (Exception ex)
        {
            XProxyLogger.ExceptionLog($"{nameof(ExchangeController)}/{nameof(CheckAndPull)}", ex,
                _telegramBotService, HttpContext.RequestAborted);
            return Json(new ExchangeResult { Result = false, Message = ex.Message });
        }
    }

    [HttpGet]
    [Authorize]
    public async Task<JsonResult> LoadRetro()
    {
        try
        {
            var exchangeService = await _exchangeServiceFactory.CreateDefaultAsync();
            return Json(await exchangeService.AV100LoadRetro(HttpContext.RequestAborted));
        }
        catch (Exception ex)
        {
            XProxyLogger.ExceptionLog($"{nameof(ExchangeController)}/{nameof(LoadRetro)}", ex,
                _telegramBotService, HttpContext.RequestAborted);
            return Json(new ExchangeResult { Result = false, Message = ex.Message });
        }
    }

    [HttpGet]
    [Authorize]
    public IActionResult StartHangfire()
    {
        _recurringJobManager.AddOrUpdate(
            "CheckAndPull",
            () => RunJob(), // use public local method only (singularity of Expression<Func<Task>> parameter)
            Cron.Minutely);

        return Redirect("/");
    }

    [HttpGet]
    public IActionResult Test()
    {
        RunJob();
        return Redirect("/");
    }

    public async Task RunJob()
    {
        try
        {
            var settings = await _userSettingsStorage.GetUserSettingsAsync(_options.DefaultUserSettingsId, CancellationToken.None);
            var result = await (await _exchangeServiceFactory.CreateDefaultAsync(CancellationToken.None))
                .AV100CheckAndLoad(CancellationToken.None);
            if ((!result.Result && settings.TelegramExtendedLog) || result.Result)
            {
                XProxyLogger.Log($"{nameof(ExchangeController)}/{nameof(RunJob)}", $"{result.Message}\r\nInserted count: {result.SucceessfulCount}",
                _telegramBotService, HttpContext.RequestAborted);
            }

        }
        catch (Exception ex)
        {
            XProxyLogger.ExceptionLog($"{nameof(ExchangeController)}/{nameof(RunJob)}", ex,
                _telegramBotService, HttpContext.RequestAborted);
        }
    }
}