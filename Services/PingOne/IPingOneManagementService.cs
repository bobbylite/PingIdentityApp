namespace PingIdentityApp.Services.PingOne;

/// <summary>
/// Represents a service for managing PingOne authentication operations.
/// </summary>
public interface IPingOneManagementService : IDisposable
{   
    /// <summary>
    /// Gets a user by their ID from PingOne.
    /// </summary>
    Task getUserByIdAsync(string userId);
}
