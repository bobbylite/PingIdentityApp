namespace PingIdentityApp.Configuration;

/// <summary>
/// Represents the configuration options for the PingOne API.
/// </summary>
/// <remarks>
/// This class represents the configuration options for the PingOne API.
/// </remarks>
public class PingOneAuthenticationOptions
{
    /// <summary>
    /// The configuration section key for the PingOne API.
    /// </summary>
    /// <value>The configuration section key for the PingOne API.</value>
    /// <remarks>
    /// This value is used to identify the configuration section for the PingOne API.
    /// </remarks>
    public const string SectionKey = "PingOneAuthentication";

    /// <summary>
    /// Gets or sets the authorization token endpoint for the PingOne application.
    /// This is the URL used to request an authorization token from the PingOne authentication server.
    /// </summary>
    /// <value>The authorization token endpoint for the PingOne application.</value>
    /// <remarks>
    /// This value is used to request an authorization token from the PingOne authentication server.
    /// </remarks>
    public string AuthorizationTokenEndpoint { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the client id for the PingOne application.
    /// This is a public key, provided by PingOne, which is used to identify your application to the PingOne authentication server.
    /// </summary>
    /// <value>The client id for the PingOne application.</value>
    /// <remarks>
    /// This value is used to identify your application to the PingOne authentication server.
    /// </remarks>
    public string ClientId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the client secret for the PingOne application.
    /// This is a private key, provided by PingOne, which is used to authenticate your application to the PingOne authentication server.
    /// </summary>
    /// <value>The client secret for the PingOne application.</value>
    /// <remarks>
    /// This value is used to authenticate your application to the PingOne authentication server.
    /// </remarks>
    public string ClientSecret { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the base URL for the PingOne application.
    /// </summary>
    /// <value>The base URL for the PingOne application.</value>
    /// <remarks>
    /// This value is used to identify the base URL for the PingOne application.
    /// </remarks>
    public string BaseUrl { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the token URL for the PingOne application.
    /// </summary>
    /// <value>The token URL for the PingOne application.</value>
    /// <remarks>
    /// This value is used to identify the token URL for the PingOne application.
    /// </remarks>
    public string TokenEndpoint { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the issuer for the PingOne application.
    /// </summary>
    /// <value>The issuer for the PingOne application.</value>
    /// <remarks>
    /// This value is used to identify the issuer for the PingOne application.
    /// </remarks>
    public string Issuer { get; set; } = string.Empty;
}


