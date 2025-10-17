using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PingIdentityApp.Models;

namespace PingIdentityApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="HomeController"/> class.
    /// </summary>
    /// <param name="logger"></param>
    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Index action to serve the home page.
    /// </summary>
    /// <returns></returns>
    public IActionResult Index()
    {
        return View();
    }

    /// <summary>
    /// Certifications action to serve the certifications page.
    /// </summary>
    /// <returns></returns>
    public IActionResult Certifications()
    {
        ViewData["Message"] = "Your certification campaigns page.";
        return View();
    }

    /// <summary>
    /// Access action to serve the access requests page.
    /// </summary>
    /// <returns></returns>
    public IActionResult Access()
    {
        ViewData["Message"] = "Your access requests page.";
        return View();
    }

    /// <summary>
    /// EmployeeResources action to serve the employee resources page.
    /// </summary>
    /// <returns></returns>
    public IActionResult EmployeeResources()
    {
        ViewData["Message"] = "Your employee resources page.";
        return View();
    }

    /// <summary>
    /// VendorResources action to serve the vendor resources page.
    /// </summary>
    /// <returns></returns>
    public IActionResult VendorResources()
    {
        ViewData["Message"] = "Your vendor resources page.";
        return View();
    }

    /// <summary>
    /// CertificationsControl action to serve the certifications control page.
    /// </summary>
    /// <returns></returns>
    public IActionResult CertificationsControl()
    {
        ViewData["Message"] = "Your certification campaigns page.";
        return View();
    }

    /// <summary>
    /// Error action to handle errors.
    /// </summary>
    /// <returns></returns>
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
