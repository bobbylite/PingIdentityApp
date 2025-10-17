using PingIdentityApp.Constants;
using PingIdentityApp.Exceptions;
using PingIdentityApp.Models;
using PingIdentityApp.Services.PingOne;

namespace PingIdentityApp.Services.Certification;

public class CertificationService : ICertificationService
{
    private readonly ILogger<CertificationService> _logger;
    private readonly IPingOneManagementService _pingOneManagementService;

    /// <summary>
    /// Initializes a new instance of the <see cref="CertificationService"/> class.
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="httpClientFactory"></param>
    /// <param name="pingOneManagementService"></param>
    public CertificationService(
        ILogger<CertificationService> logger,
        IPingOneManagementService pingOneManagementService
    )
    {
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(pingOneManagementService);

        _logger = logger;
        _pingOneManagementService = pingOneManagementService;

        AccessRequests = [];
        AccessRequestsHistory = [];
    }

    /// <inheritdoc />
    public List<AccessRequest> AccessRequests { get; set; }

    /// <inheritdoc />
    public List<AccessRequest> AccessRequestsHistory { get; set; }

    /// <inheritdoc />
    public async Task ApproveAccessRequestAsync(string requestId)
    {
        var request = AccessRequests.SingleOrDefault(r => r.Id == requestId);
        var userId = request?.UserId ?? throw new NullOrEmptyTokenException("UserId is null or empty");
        var groupId = request?.GroupId ?? throw new NullOrEmptyTokenException("GroupId is null or empty");
        request.Status = AccessRequestStatus.Approved;
        AccessRequestsHistory.Add(request);
        AccessRequests.Remove(request);

        await _pingOneManagementService.ProvisionGroupMembershipAsync(userId, groupId);

        _logger.LogInformation("Approved access request {RequestId} for user {UserId} to join group {GroupId}", requestId, userId, groupId);
    }

    /// <inheritdoc />
    public Task CreateAccessRequestAsync(string userId, string groupId)
    {
        var newAccessRequest = new AccessRequest
        {
            Id = Guid.NewGuid().ToString(),
            UserId = userId,
            GroupId = groupId,
            RequestedAt = DateTime.UtcNow,
            Status = AccessRequestStatus.Pending
        };

        AccessRequests.Add(newAccessRequest);

        _logger.LogInformation("Created access request {RequestId} for user {UserId} to join group {GroupId}", newAccessRequest.Id, userId, groupId);
        return Task.CompletedTask;
    }

    /// <inheritdoc />
    public Task DenyAccessRequestAsync(string requestId)
    {
        var request = AccessRequests.SingleOrDefault(r => r.Id == requestId);
        var userId = request?.UserId ?? throw new NullOrEmptyTokenException("UserId is null or empty");
        var groupId = request?.GroupId ?? throw new NullOrEmptyTokenException("GroupId is null or empty");
        request.Status = AccessRequestStatus.Denied;
        AccessRequestsHistory.Add(request);
        AccessRequests.Remove(request);

        _logger.LogInformation("Denied access request {RequestId} for user {UserId} to join group {GroupId}", requestId, userId, groupId);

        return Task.CompletedTask;
    }
}