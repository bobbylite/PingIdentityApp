using PingIdentityApp.Models;

namespace PingIdentityApp.Services.PingOne;

/// <summary>
/// Represents a service for managing PingOne authentication operations.
/// </summary>
public interface IPingOneManagementService : IDisposable
{
    /// <summary>
    /// Gets a group by it's unique identifier.
    /// </summary>
    Task<IEnumerable<Group>> GetGroupsAsync();
    
    /// <summary>
    /// Gets a user by their unique identifier.
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="groupId"></param>
    /// <returns></returns>
    Task ProvisionGroupMembershipAsync(string userId, string groupId);
}
