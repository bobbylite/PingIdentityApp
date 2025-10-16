namespace PingIdentityApp.Constants;

/// <summary>
/// Constants representing the names of HTTP clients.
/// </summary>
public static class HttpClientNames
{
    /// <summary>
    /// The name of the HTTP client used to communicate with the Authorization API.
    /// </summary>
    public const string PingOneApi = "PingOne.Api.Authentication.Token";

    /// <summary>
    /// The name of the HTTP client used to communicate with the PingOne Management API.
    /// </summary>
    public const string PingOneManagementApi = "PingOne.Api.Management";
}
