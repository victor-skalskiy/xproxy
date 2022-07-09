using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using XProxy.DAL;
using XProxy.Web.Models;
using XProxy.Interfaces;

namespace XProxy.Web.Controllers;

public class SettingsController : Controller
{
    private readonly ILogger<SettingsController> _logger;
    private readonly ISettingsService _settingsService;

    public SettingsController(ILogger<SettingsController> logger, ISettingsService settingsService)
    {
        _settingsService = settingsService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var result = await _settingsService.GetSettingsAsync(HttpContext.RequestAborted);

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
        var userSettings = await _settingsService.CreateUserSettingsAsync(model.UserUpdateInterval, model.Av100Token, model.XLAPIUrl, model.XLToken, HttpContext.RequestAborted);
        return RedirectToAction("Index");
    }



    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

