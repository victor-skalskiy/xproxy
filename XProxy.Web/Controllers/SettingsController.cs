using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using XProxy.Web.Models;
using XProxy.Interfaces;
using Polly;
using System;
using Newtonsoft.Json;

namespace XProxy.Web.Controllers;

public class SettingsController : Controller
{
    private readonly ILogger<SettingsController> _logger;
    private readonly ISettingsService _settingsService;
    private readonly IFiltersService _filtersService;
    private readonly IExchangeService _exchangeService;

    public SettingsController(ILogger<SettingsController> logger, ISettingsService settingsService, IFiltersService filtersService,
        IExchangeService exchangeService)
    {
        _settingsService = settingsService;
        _logger = logger;
        _filtersService = filtersService;
        _exchangeService = exchangeService;
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public IActionResult Privacy() => View();

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var userSettingsId = 1;

        return View(new IndexModel
        {
            Settings = await _settingsService.GetSettingsAsync(HttpContext.RequestAborted),
            Filters = await _filtersService.GetFiltersAsync(HttpContext.RequestAborted),
            Profile = await _exchangeService.AV100RequestProfile(userSettingsId, HttpContext.RequestAborted)
        });
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        return View("Create", new SettingsEditModel() { });
    }

    [HttpPost]
    public async Task<IActionResult> Create(SettingsEditModel model)
    {
        var resutl = await _settingsService.CreateUserSettingsAsync(model.UpdateInterval, model.Av100Token, model.XLombardAPIUrl, model.XLombardToken,
            model.XLombardFilialId, model.XLombardDealTypeId, model.XLombardSource, HttpContext.RequestAborted);

        return Redirect("/");
    }

    [HttpGet]
    public async Task<IActionResult> Edit(long id)
    {
        var result = await _settingsService.GetSettingsItemAsync(id, HttpContext.RequestAborted);
        return View("Edit", new SettingsEditModel
        {
            Av100Token = result.AV100Token,
            UpdateInterval = result.UpdateInterval,
            XLombardAPIUrl = result.XLombardAPIUrl,
            XLombardToken = result.XLombardToken,
            XLombardDealTypeId = result.XLombardDealTypeId,
            XLombardFilialId = result.XLombardFilialId,
            XLombardSource = result.XLombardSource
        });
    }

    [HttpPost]
    public async Task<IActionResult> Edit(SettingsEditModel model, long id)
    {
        var resutl = await _settingsService.UpdateUserSettingsAsync(id, model.UpdateInterval, model.Av100Token, model.XLombardAPIUrl, model.XLombardToken,
            model.XLombardFilialId, model.XLombardDealTypeId, model.XLombardSource, HttpContext.RequestAborted);

        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> CheckConnection()
    {
        var result = await _exchangeService.XLRequest(1, HttpContext.RequestAborted);

        return result.state > 0 ? RedirectToAction("Index") : RedirectToAction("Error");
    }

    [HttpGet]
    public async Task<IActionResult> LoadDictionaries()
    {
        var regions = await _exchangeService.AV100RequestRegions(HttpContext.RequestAborted);
        if (regions is not null)
            await _filtersService.CreateRegionsAsync(regions.ToDictionary(z => z.RegionId, z => z.Name), HttpContext.RequestAborted);

        var sources = await _exchangeService.AV100RequestSource(HttpContext.RequestAborted);
        if (sources is not null)
            await _filtersService.CreateSourcesAsync(sources.ToDictionary(z => z.SourceId, z => z.Name), HttpContext.RequestAborted);

        return RedirectToAction("Index");
    }
}