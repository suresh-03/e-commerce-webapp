using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using e_commerce_website.Models;

namespace e_commerce_website.Controllers;

public class JSController : Controller
{
    private readonly ILogger<JSController> _logger;

    public JSController(ILogger<JSController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
