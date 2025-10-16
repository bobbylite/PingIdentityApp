using Microsoft.Extensions.Options;
using PingIdentityApp.Configuration;
using PingIdentityApp.Services.Token;

namespace PingIdentityApp.Services.PingOne;

public class PingOneManagementService : IPingOneManagementService
{
    private readonly ILogger<PingOneManagementService> _logger;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IOptionsMonitor<PingOneAuthenticationOptions> _optionsMonitor;
    private readonly ITokenService _tokenService;

    /// <summary>
    /// Initializes a new instance of the <see cref="PingOneManagementService"/> class.
    /// </summary>
    /// <param name="logger">The logger.</param>
    /// <param name="httpClientFactory">The HTTP client factory.</param>
    /// <param name="optionsMonitor">The options monitor.</param>
    /// <param name="tokenService">The token service.</param>
    /// <exception cref="ArgumentNullException">Thrown if any of the arguments are null.</exception>
    public PingOneManagementService(
        ILogger<PingOneManagementService> logger,
        IHttpClientFactory httpClientFactory,
        IOptionsMonitor<PingOneAuthenticationOptions> optionsMonitor,
        ITokenService tokenService)
    {
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(httpClientFactory);
        ArgumentNullException.ThrowIfNull(optionsMonitor);
        ArgumentNullException.ThrowIfNull(tokenService);

        _logger = logger;
        _httpClientFactory = httpClientFactory;
        _optionsMonitor = optionsMonitor;
        _tokenService = tokenService;
    }

    /// <inheritdoc />
    public async Task getUserByIdAsync(string userId)
    {
        ArgumentNullException.ThrowIfNull(userId);

        await _tokenService.AuthenticateAsync();
    }

    /// <inheritdoc />
    public void Dispose()
    {
        _tokenService.Dispose();
    }
}
