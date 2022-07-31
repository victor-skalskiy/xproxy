using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using XProxy.Web.Models;
using XProxy.Interfaces;
using Polly;
using System;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace XProxy.Web.Controllers;

public class FilterController : Controller
{
    private readonly ILogger<SettingsController> _logger;
    private readonly ISettingsService _settingsService;
    private readonly IFiltersService _filtersService;

    public FilterController(ILogger<SettingsController> logger, ISettingsService settingsService, IFiltersService filtersService)
    {
        _settingsService = settingsService;
        _logger = logger;
        _filtersService = filtersService;
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    private async void AddViewBagOptions()
    {
        ViewBag.Regions = await _filtersService.GetRegionsAsync(HttpContext.RequestAborted);
        ViewBag.Sources = new SelectList(await _filtersService.GetSourcesAsync(HttpContext.RequestAborted), "Id", "Name");
    }


    [HttpGet]
    public async Task<IActionResult> Create()
    {
        AddViewBagOptions();

        return View("Create", new FilterEditModel() { });
    }

    [HttpPost]
    public async Task<IActionResult> Create(FilterEditModel model)
    {
        var result = await _filtersService.CreateFilterAsync(model.YearStart, model.YearEnd, model.PriceStart, model.PriceEnd,
            model.DistanceStart, model.DistanceEnd, model.CarCount, model.PhoneCount, model.RegionIds, model.SourceIds,
            HttpContext.RequestAborted);

        return Redirect("/");
    }

    [HttpGet]
    public async Task<IActionResult> Edit(long id)
    {
        var model = await _filtersService.GetFilterAsync(id, HttpContext.RequestAborted);

        ViewBag.Regions = model.AllRegions;
        ViewBag.Sources = model.AllSources;

        return View("Edit", new FilterEditModel
        {
            YearStart = model.YearStart,
            YearEnd = model.YearEnd,
            PriceStart = model.PriceStart,
            PriceEnd = model.PriceEnd,
            DistanceStart = model.DistanceStart,
            DistanceEnd = model.DistanceEnd,
            CarCount = model.CarCount,
            PhoneCount = model.PhoneCount,
            RegionIds = model.RegionIds,
            SourceIds = model.SourceIds
        });
    }

    [HttpPost]
    public async Task<IActionResult> Edit(FilterEditModel model, long id)
    {
        var result = await _filtersService.UpdateFilterAsync(id, model.YearStart, model.YearEnd, model.PriceStart, model.PriceEnd,
            model.DistanceStart, model.DistanceEnd, model.CarCount, model.PhoneCount, model.RegionIds, model.SourceIds,
            HttpContext.RequestAborted);

        return Redirect("/");
    }
}