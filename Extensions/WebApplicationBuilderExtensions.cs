using PingIdentityApp.Configuration;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;
using PingIdentityApp.Http;
using PingIdentityApp.Services.Token;
using PingIdentityApp.Constants;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using PingIdentityApp.Services.PingOne;
using PingIdentityApp.Services.Certification;
using Microsoft.EntityFrameworkCore;
using GetChatty.Data;

namespace PingIdentityApp.Extensions;

public static class WebApplicationBuilderExtensions
{
    /// <summary>
    /// Add support for persisting data to a PostgreSQL database in Azure.
    /// </summary>
    /// <param name="webApplicationBuilder">A builder for web applications and services.</param>
    /// <returns>A reference to this instance after the operation has completed.</returns>
    public static WebApplicationBuilder AddDataPersistence(this WebApplicationBuilder webApplicationBuilder)
    {
        ArgumentNullException.ThrowIfNull(webApplicationBuilder);

        var connectionString = webApplicationBuilder.Configuration.GetConnectionString("PostgreSqlConnection");

        webApplicationBuilder.Services.AddDbContext<GetChattyDataContext>(opt =>
            opt.UseNpgsql(connectionString));
        webApplicationBuilder.Services.AddDatabaseDeveloperPageExceptionFilter();

        return webApplicationBuilder;
    }
    
    /// <summary>
    /// Add services to the application.
    /// </summary>
    /// <param name="webApplicationBuilder">A builder for web applications and services.</param>
    /// <returns>A reference to this instance after the operation has completed.</returns>
    /// <remarks>
    /// This method adds services to the application.
    /// </remarks>
    public static WebApplicationBuilder AddServices(this WebApplicationBuilder webApplicationBuilder)
    { 
        ArgumentNullException.ThrowIfNull(webApplicationBuilder);

        webApplicationBuilder.Services.AddServiceDiscovery();

        webApplicationBuilder.Services.ConfigureHttpClientDefaults(http =>
        {
            // Turn on service discovery by default
            http.AddServiceDiscovery();
        });

        // Add a singleton service of type ITokenService. The same instance of TokenService will be used every time ITokenService is requested.
        webApplicationBuilder.Services.AddSingleton<ITokenService, TokenService>();
        
        webApplicationBuilder.Services.AddTransient<ICertificationService, CertificationService>();
        webApplicationBuilder.Services.AddTransient<IPingOneManagementService, PingOneManagementService>();
        
        webApplicationBuilder.Services.AddHttpContextAccessor();
        webApplicationBuilder.Services.AddControllersWithViews();

        return webApplicationBuilder;
    }

    /// <summary>
    /// Add an HttpClient for the PingIdentityApp REST API. Includes handling authentication for the API by passing the access
    /// token for the currently logged in user as a JWT bearer token to the API.
    /// </summary>
    /// <param name="webApplicationBuilder">A builder for web applications and services.</param>
    /// <returns>A reference to this instance after the operation has completed of type <see cref="WebApplicationBuilder"/></returns>
    /// <remarks>
    /// This method adds an HttpClient for the PingIdentityApp REST API.
    /// </remarks>
    public static WebApplicationBuilder AddPingIdentityAppApiClients(this WebApplicationBuilder webApplicationBuilder)
    {
        ArgumentNullException.ThrowIfNull(webApplicationBuilder);
        
        webApplicationBuilder.Services.AddOptionsWithValidateOnStart<PingOneAuthenticationOptions>()
            .BindConfiguration(PingOneAuthenticationOptions.SectionKey);

        webApplicationBuilder.Services.AddTransient<AuthenticationHandler>()
        .AddHttpClient(HttpClientNames.PingOneApi, (provider, client) =>
        {
            var apiOptionsSnapshot = provider.GetRequiredService<IOptionsMonitor<PingOneAuthenticationOptions>>();
            var apiOptions = apiOptionsSnapshot.CurrentValue;
            client.BaseAddress = new Uri($"{apiOptions.BaseUrl}"); 
        })
        .AddHttpMessageHandler<AuthenticationHandler>();
        
        return webApplicationBuilder;
    }

    /// <summary>
    /// Add support for refreshing access tokens using refresh tokens.
    /// With .NET 8, the configuration for this can be controled completely from 'appsettings.json'.
    /// </summary>
    /// <param name="webApplicationBuilder">A builder for web applications and services.</param>
    /// <returns>A reference to this instance after the operation has completed.</returns>
    /// <remarks>
    /// This method adds support for refreshing access tokens using refresh tokens.
    /// </remarks>
    public static WebApplicationBuilder AddAccessControl(this WebApplicationBuilder webApplicationBuilder)
    {
        ArgumentNullException.ThrowIfNull(webApplicationBuilder);

        webApplicationBuilder.Services.AddAuthentication(options =>
        {
            options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
        })
        .AddCookie()
        .AddOpenIdConnect(options =>
        {
            var oidcOptions = webApplicationBuilder.Configuration
                .GetSection("Authentication:Schemes:OpenIdConnect")
                .Get<OpenIdConnectOptions>();
            ArgumentNullException.ThrowIfNull(oidcOptions);
            
            options.Authority = oidcOptions.Authority; // Your OIDC issuer URL
            options.ClientId = oidcOptions.ClientId;
            options.ClientSecret = oidcOptions.ClientSecret;

            options.ResponseType = OpenIdConnectResponseType.Code;

            options.Scope.Clear();
            options.Scope.Add("openid");
            options.Scope.Add("profile");
            options.Scope.Add("email");

            options.CallbackPath = "/signin-oidc";
            options.SignedOutCallbackPath = "/signout-callback-oidc";
            options.SignedOutRedirectUri = "/";

            options.SaveTokens = true;

            options.TokenValidationParameters.NameClaimType = "name";
            options.TokenValidationParameters.RoleClaimType = "role";

            options.Events = new OpenIdConnectEvents
            {
                OnAuthenticationFailed = ctx =>
                {
                    Console.WriteLine($"OIDC Auth failed: {ctx.Exception}");
                    return Task.CompletedTask;
                },
                OnTokenValidated = ctx =>
                {
                    Console.WriteLine($"OIDC Token validated for {ctx.Principal?.Identity?.Name}");
                    return Task.CompletedTask;
                },
                OnRemoteFailure = context =>
                {
                    if (context.Failure is OpenIdConnectProtocolException ex && 
                        ex.Message.Contains("access_denied"))
                    {
                        context.Response.Redirect("/AccessDenied");
                        context.HandleResponse(); 
                    }
                    return Task.CompletedTask;
                }
            };
        });

        webApplicationBuilder.Services.AddAuthorization();
        
        return webApplicationBuilder;
    }
}
