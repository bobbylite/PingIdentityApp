using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;

namespace PingIdentityApp.Controllers;

[Route("account")]
public class AccountController : Controller
{
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
}
