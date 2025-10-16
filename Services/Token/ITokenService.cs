namespace PingIdentityApp.Services.Token;

/// <summary>
/// Represents a service for performing authentication operations with PingOne.
/// </summary>
public interface ITokenService : IDisposable
{
    /// <summary>
    /// The bearer token returned by a previously successful authentication operation.
    /// </summary>
    string Token { get; set; }
    
    /// <summary>
    /// Performs an asynchronous authentication operation with PingOne.
    /// </summary>
    Task AuthenticateAsync();
}
