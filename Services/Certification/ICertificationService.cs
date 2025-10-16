using PingIdentityApp.Models;

namespace PingIdentityApp.Services.Certification;

public interface ICertificationService
{
    /// <summary>
    /// Gets or sets the access requests.
    /// </summary>
    List<AccessRequest> AccessRequests { get; set; }

    /// <summary>
    /// Creates an access request for a user to join a group.
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="groupId"></param>
    /// <returns></returns>
    Task CreateAccessRequestAsync(string userId, string groupId);

    /// <summary>
    /// Approves an access request.
    /// </summary>
    /// <param name="requestId"></param>
    /// <returns></returns>
    Task ApproveAccessRequestAsync(string requestId);

    /// <summary>
    /// Denies an access request.
    /// </summary>
    /// <param name="requestId"></param>
    /// <returns></returns>
    Task DenyAccessRequestAsync(string requestId);
}