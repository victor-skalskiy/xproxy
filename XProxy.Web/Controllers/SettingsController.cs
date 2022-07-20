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
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IFiltersService _filtersService;

    public SettingsController(ILogger<SettingsController> logger, ISettingsService settingsService, IHttpClientFactory httpClientFactory,
        IFiltersService filtersService)
    {
        _settingsService = settingsService;
        _logger = logger;
        _httpClientFactory = httpClientFactory;
        _filtersService = filtersService;
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

        var result = new Dictionary<string, object>();
        result.Add("Settings", await _settingsService.GetSettingsAsync(HttpContext.RequestAborted));
        result.Add("Filter", await _filtersService.GetFiltersAsync(HttpContext.RequestAborted));
        result.Add("Status", await _settingsService.AV100RequestProfile(userSettingsId, HttpContext.RequestAborted));

        return View(result);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        return View("Edit", new SettingsEditModel() { });
    }

    [HttpPost]
    public async Task<IActionResult> Create(SettingsEditModel model)
    {
        var resutl = await _settingsService.CreateUserSettingsAsync(model.UpdateInterval, model.Av100Token, model.XLombardAPIUrl, model.XLombardToken,
            model.XLombardFilialId, model.XLombardDealTypeId, model.XLombardSource, HttpContext.RequestAborted);

        return RedirectToAction("Index");
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
        var result = await _settingsService.XLRequest(1, HttpContext.RequestAborted);

        return result.state > 0 ? RedirectToAction("Index") : RedirectToAction("Error");
    }

    public async Task<IActionResult> TestFire()
    {


        return RedirectToAction("Index");
    }

}