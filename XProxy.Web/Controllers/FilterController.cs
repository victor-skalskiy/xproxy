﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using XProxy.Web.Models;
using XProxy.Interfaces;
using Polly;
using System;
using Newtonsoft.Json;

namespace XProxy.Web.Controllers;

public class FilterController : Controller
{
    private readonly ILogger<SettingsController> _logger;
    private readonly ISettingsService _settingsService;
    private readonly IHttpClientFactory _httpClientFactory;

    public FilterController(ILogger<SettingsController> logger, ISettingsService settingsService, IHttpClientFactory httpClientFactory)
    {
        _settingsService = settingsService;
        _logger = logger;
        _httpClientFactory = httpClientFactory;
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }



    [HttpGet]
    public async Task<IActionResult> Create()
    {
        return View("Edit", new FilterEditModel() { });        
    }

    [HttpPost]
    public async Task<IActionResult> Create(SettingsEditModel model)
    {
        //var resutl = await _settingsService.CreateUserSettingsAsync(model.UpdateInterval, model.Av100Token, model.XLombardAPIUrl, model.XLombardToken,
        //    model.XLombardFilialId, model.XLombardDealTypeId, model.XLombardSource, HttpContext.RequestAborted);

        //return RedirectToAction("Index");
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Edit(long id)
    {
        //var result = await _settingsService.GetSettingsItemAsync(id, HttpContext.RequestAborted);
        //return View("Edit", new SettingsEditModel
        //{
        //    Av100Token = result.Av100Token,
        //    UpdateInterval = result.UpdateInterval,
        //    XLombardAPIUrl = result.XLombardAPIUrl,
        //    XLombardToken = result.XLombardToken,
        //    XLombardDealTypeId = result.XLombardDealTypeId,
        //    XLombardFilialId = result.XLombardFilialId,
        //    XLombardSource = result.XLombardSource
        //});
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Edit(SettingsEditModel model, long id)
    {
        //var resutl = await _settingsService.UpdateUserSettingsAsync(id, model.UpdateInterval, model.Av100Token, model.XLombardAPIUrl, model.XLombardToken,
        //    model.XLombardFilialId, model.XLombardDealTypeId, model.XLombardSource, HttpContext.RequestAborted);

        //return RedirectToAction("Index");
        return View();
    }    
}