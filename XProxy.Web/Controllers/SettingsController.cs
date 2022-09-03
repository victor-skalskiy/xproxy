using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using XProxy.Web.Models;
using XProxy.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Hangfire;

namespace XProxy.Web.Controllers;

public class SettingsController : Controller
{
    private readonly ILogger<SettingsController> _logger;
    private readonly ISettingsService _settingsService;
    private readonly IFiltersService _filtersService;
    private readonly IExchangeServiceFactory _exchangeServiceFactory;
    private readonly IXProxyOptions _options;

    public SettingsController(
        ISettingsService settingsService,
        IFiltersService filtersService,
        IExchangeServiceFactory exchangeServiceFactory,
        IXProxyOptions xProxyOptions,
        ILogger<SettingsController> logger)
    {
        _settingsService = settingsService;
        _filtersService = filtersService;
        _exchangeServiceFactory = exchangeServiceFactory;
        _options = xProxyOptions;
        _logger = logger;
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Index()
    {
        var exchangeService =
            await _exchangeServiceFactory.CreateAsync(_options.DefaultUserSettingsId, _options.DefaultFilterId, HttpContext.RequestAborted);

        return View(new IndexModel
        {
            Settings = await _settingsService.GetSettingsAsync(HttpContext.RequestAborted),
            Filters = await _filtersService.GetFiltersAsync(HttpContext.RequestAborted),
            Profile = await exchangeService.AV100RequestProfile(HttpContext.RequestAborted)
        });
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Create()
    {
        return View("Create", new SettingsEditModel());
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create(SettingsEditModel model)
    {
        var resutl = await _settingsService.CreateUserSettingsAsync(model.Av100Token, model.XLombardAPIUrl, model.XLombardToken,
            model.XLombardFilialId, model.XLombardDealTypeId, model.XLombardSource, model.TelegramBotToken, model.TelegramAdminChatId,
            HttpContext.RequestAborted);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Edit(long id)
    {
        var result = await _settingsService.GetSettingsItemAsync(id, HttpContext.RequestAborted);
        return View("Edit", new SettingsEditModel
        {
            Av100Token = result.AV100Token,
            XLombardAPIUrl = result.XLombardAPIUrl,
            XLombardToken = result.XLombardToken,
            XLombardDealTypeId = result.XLombardDealTypeId,
            XLombardFilialId = result.XLombardFilialId,
            XLombardSource = result.XLombardSource,
            TelegramBotToken = result.TelegramBotToken,
            TelegramAdminChatId = result.TelegramAdminChatId
        });
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Edit(SettingsEditModel model, long id)
    {
        var resutl = await _settingsService.UpdateUserSettingsAsync(id, model.Av100Token, model.XLombardAPIUrl, model.XLombardToken,
            model.XLombardFilialId, model.XLombardDealTypeId, model.XLombardSource, model.TelegramBotToken, model.TelegramAdminChatId,
            HttpContext.RequestAborted);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> LoadDictionaries()
    {
        var exchangeService = await _exchangeServiceFactory.CreateDefaultAsync();

        var regions = await exchangeService.AV100RequestRegions(HttpContext.RequestAborted);
        if (regions is not null)
            await _filtersService.CreateRegionsAsync(regions.ToDictionary(z => z.RegionId, z => z.Name), HttpContext.RequestAborted);

        var sources = await exchangeService.AV100RequestSource(HttpContext.RequestAborted);
        if (sources is not null)
            await _filtersService.CreateSourcesAsync(sources.ToDictionary(z => z.SourceId, z => z.Name), HttpContext.RequestAborted);

        return RedirectToAction(nameof(Index));
    }
}