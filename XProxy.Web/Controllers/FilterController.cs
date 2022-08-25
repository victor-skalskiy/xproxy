using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using XProxy.Web.Models;
using XProxy.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using XProxy.Domain;

namespace XProxy.Web.Controllers;

public class FilterController : Controller
{
    private readonly ILogger<SettingsController> _logger;
    private readonly ISettingsService _settingsService;
    private readonly IFiltersService _filtersService;
    private readonly IExchangeServiceFactory _exchangeServiceFactory;

    public FilterController(
        ILogger<SettingsController> logger,
        ISettingsService settingsService,
        IFiltersService filtersService,
        IExchangeServiceFactory exchangeServiceFactory)
    {
        _settingsService = settingsService;
        _logger = logger;
        _filtersService = filtersService;
        _exchangeServiceFactory = exchangeServiceFactory;
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var model = new FilterEditModel
        {
            Regions = new MultiSelectList(
                await _filtersService.GetRegionsAsync(HttpContext.RequestAborted),
                nameof(AV100Region.Id),
                nameof(AV100Region.Name)),
            Sources = new MultiSelectList(
                await _filtersService.GetSourcesAsync(HttpContext.RequestAborted),
                nameof(AV100Source.Id),
                nameof(AV100Source.Name))
        };

        return View("Create", model);
    }

    [HttpPost]
    public async Task<IActionResult> Create(FilterEditModel model)
    {
        if (ModelState.IsValid)
        {
            var result = await _filtersService.CreateFilterAsync(model.YearStart, model.YearEnd, model.PriceStart, model.PriceEnd,
                model.DistanceStart, model.DistanceEnd, model.PackCount, model.Remont, model.RegionIds.ToList(), model.SourceIds.ToList(),
                HttpContext.RequestAborted);

            return RedirectToAction("Index", "Settings");
        }

        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(long id)
    {
        var exchangeService = await _exchangeServiceFactory.CreateDefaultAsync(HttpContext.RequestAborted);
        var model = await _filtersService.GetFilterAsync(id, HttpContext.RequestAborted);

        return View("Edit", new FilterEditModel
        {
            YearStart = model.YearStart,
            YearEnd = model.YearEnd,
            PriceStart = model.PriceStart,
            PriceEnd = model.PriceEnd,
            DistanceStart = model.DistanceStart,
            DistanceEnd = model.DistanceEnd,
            PackCount = model.PackCount,
            Remont = model.Remont,
            RegionIds = model.RegionIds,
            SourceIds = model.SourceIds,
            RegionExternalIds = model.RegionExternalIds,
            SourceExternalIds = model.SourceExternalIds,
            Regions = new MultiSelectList(
                model.AllRegions,
                nameof(AV100Region.Id),
                nameof(AV100Region.Name),
                model.RegionIds),
            Sources = new MultiSelectList(
                model.AllSources,
                nameof(AV100Source.Id),
                nameof(AV100Source.Name),
                model.SourceIds),
            FilterRequestString = await exchangeService.AV100RequestString(HttpContext.RequestAborted),
            FilterRequestCounter = await exchangeService.AV100ReuestListCount(0, 0, HttpContext.RequestAborted)
        });
    }

    [HttpPost]
    public async Task<IActionResult> Edit(FilterEditModel model, long id)
    {
        if (ModelState.IsValid)
        {
            var result = await _filtersService.UpdateFilterAsync(id, model.YearStart, model.YearEnd, model.PriceStart, model.PriceEnd,
                model.DistanceStart, model.DistanceEnd, model.PackCount, model.Remont, model.RegionIds.ToList(), model.SourceIds.ToList(),
                HttpContext.RequestAborted);

            return RedirectToAction("Index", "Settings");
        }

        return View(model);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}