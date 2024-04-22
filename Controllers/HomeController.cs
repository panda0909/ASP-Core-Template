using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ASP_Core_Template.Models;
using Microsoft.Extensions.Options;
using ASP_Core_Template.Options;

namespace ASP_Core_Template.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IWebHostEnvironment _env;
    private readonly TemplateOption _options;

    public HomeController(ILogger<HomeController> logger
        , IOptions<TemplateOption> options, IWebHostEnvironment env)
    {
        _logger = logger;
        _options = options.Value;
        _env = env;
    }

    public IActionResult Index()
    {
        //選項模式
        ViewBag.key1 = _options.key1;
        ViewBag.key2 = _options.key2;

        
        _logger.LogInformation("Home Page");
        _logger.LogDebug("Debug Home Page");
        _logger.LogWarning("Warning Home Page");
        _logger.LogError("Error Home Page");
        _logger.LogCritical("Critical Home Page");

        // env
        _logger.LogInformation("Env : " + _env.EnvironmentName);
        return View();
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
