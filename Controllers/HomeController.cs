using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PingIdentityApp.Models;
using PingIdentityApp.Services.PingOne;

namespace PingIdentityApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IPingOneManagementService _pingOneManagementService;

    /// <summary>
    /// Initializes a new instance of the <see cref="HomeController"/> class.
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="pingOneManagementService"></param>
    public HomeController(
        ILogger<HomeController> logger,
        IPingOneManagementService pingOneManagementService)
    {
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(pingOneManagementService);

        _logger = logger;
        _pingOneManagementService = pingOneManagementService;
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
    /// Reservations action to serve the reservations page.
    /// </summary>
    /// <returns></returns>
    [Authorize]
    public IActionResult Reservations()
    {
        ViewData["Message"] = "Your reservations page.";
        return View();
    }

    /// <summary>
    /// IdentityProofing action to handle identity proofing form submission.
    /// </summary>
    /// <param name="reservationForm"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> IdentityProofing([FromForm] ReservationForm reservationForm)
    {
        var transaction = await _pingOneManagementService
            .CreateVerifyTransactionAsync(reservationForm.UserId!, reservationForm.DateOfBirth!, reservationForm.Address!);

        return View("BeginIdentityProofing", transaction);
    }

    /// <summary>
    /// GetTransactionStatus action to check the status of a transaction.
    /// </summary>
    /// <param name="transactionId"></param>
    /// <returns></returns>
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetTransactionStatus(string transactionId)
    {
        var userId = User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        var status = await _pingOneManagementService.GetVerifyTransactionAsync(userId!, transactionId);
        return Json(status);
    }

    /// <summary>
    /// CreateReservation action to serve the create reservation page.
    /// </summary>
    /// <returns></returns>
    [Authorize]
    public IActionResult CreateReservation()
    {
        return View();
    }

    /// <summary>
    /// CreateReservation action to handle reservation form submission.
    /// </summary>
    /// <param name="reservationForm"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize]
    public IActionResult CreateReservation([FromForm] ReservationPayment reservationForm)
    {
        return View("ReservationPayment", reservationForm);
    }

    /// <summary>
    /// ReservationConfirmation action to handle reservation confirmation after payment.
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize]
    public IActionResult ReservationConfirmation(ReservationPayment model)
    {
        if (model == null)
        {
            return BadRequest("Invalid reservation data.");
        }

        var rng = new Random();
        var confirmationCode = rng.Next(100000, 999999).ToString();

        ViewData["ConfirmationCode"] = confirmationCode;
        return View("ReservationConfirmation", model);
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
