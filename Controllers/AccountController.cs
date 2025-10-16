using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PingIdentityApp.Services.Certification;
using PingIdentityApp.Services.PingOne;

namespace PingIdentityApp.Controllers;

[Route("account")]
public class AccountController : Controller
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ICertificationService _certificationService;

    /// <summary>
    /// Initializes a new instance of the <see cref="AccountController"/> class.
    /// </summary>
    /// <param name="httpContextAccessor"></param>
    /// <param name="certificationService"></param>
    public AccountController(
        IHttpContextAccessor httpContextAccessor,
        ICertificationService certificationService)
    {
        ArgumentNullException.ThrowIfNull(httpContextAccessor);
        ArgumentNullException.ThrowIfNull(certificationService);

        _httpContextAccessor = httpContextAccessor;
        _certificationService = certificationService;
    }

    [HttpGet("login")]
    public IActionResult Login(string? returnUrl = "/")
    {
        // Validate or sanitize returnUrl if you accept it from the querystring in prod
        var props = new AuthenticationProperties { RedirectUri = returnUrl };
        return Challenge(props, OpenIdConnectDefaults.AuthenticationScheme);
    }

    [HttpGet("logout")]
    public IActionResult Logout()
    {
        // sign out from cookie first and then trigger OP end session
        var props = new AuthenticationProperties { RedirectUri = "/" };
        return SignOut(props, OpenIdConnectDefaults.AuthenticationScheme, CookieAuthenticationDefaults.AuthenticationScheme);
    }

    [Authorize]
    [HttpPost("request-access")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> RequestAccess([FromForm] string groupId)
    {
        if (string.IsNullOrWhiteSpace(groupId))
        {
            TempData["Message"] = "Invalid group selection.";
            return Redirect(Request.Headers["Referer"].ToString());
        }

        var user = _httpContextAccessor.HttpContext?.User;
        var userId = user?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

        if (userId == null)
        {
            TempData["Message"] = "User context not found. Please log in again.";
            return RedirectToAction("Login");
        }

        try
        {
            await _certificationService.CreateAccessRequestAsync(userId, groupId);
            TempData["Message"] = "Access request submitted successfully!";
        }
        catch (Exception ex)
        {
            TempData["Message"] = $"Error requesting access: {ex.Message}";
        }

        //return Redirect(Request.Headers["Referer"].ToString());
        return RedirectToAction("Access", "Home");
    }
}
