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
    /// Gets all users.
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<PingOneUser>> GetUsersAsync();

    /// <summary>
    /// Gets a user by their unique identifier.
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<PingOneUser> GetUserByIdAsync(string userId);

    /// <summary>
    /// Gets a group by it's unique identifier.
    /// </summary>
    /// <param name="groupId"></param>
    /// <returns></returns>
    Task<Group> GetGroupByIdAsync(string groupId);

    /// <summary>
    /// Gets a user's group membership by their unique identifier.
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<IEnumerable<GroupMembership>> GetUsersGroupMembershipAsync(string userId);

    /// <summary>
    /// Gets a user by their unique identifier.
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="groupId"></param>
    /// <returns></returns>
    Task ProvisionGroupMembershipAsync(string userId, string groupId);

    /// <summary>
    /// Deprovisions a user's group membership by their unique identifier.
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="groupId"></param>
    /// <returns></returns>
    Task DeprovisionGroupMembershipAsync(string userId, string groupId);

    /// <summary>
    /// Creates a Verify transaction for the specified user.
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="dateOfBirth"></param>
    /// <param name="address"></param>
    /// <returns></returns>
    Task<VerifyTransactionCreatedResponse> CreateVerifyTransactionAsync(string userId, string dateOfBirth, string address);

    /// <summary>
    /// Gets a Verify transaction by its unique identifier.
    /// </summary>
    /// <param name="transactionId"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<VerifyTransactionResponse> GetVerifyTransactionAsync(string userId, string transactionId);
}
