using Microsoft.EntityFrameworkCore;
using GetChatty.Data;
using PingIdentityApp.Constants;
using PingIdentityApp.Exceptions;
using PingIdentityApp.Models;
using PingIdentityApp.Services.PingOne;

namespace PingIdentityApp.Services.Certification;

public class CertificationService : ICertificationService
{
    private readonly ILogger<CertificationService> _logger;
    private readonly IPingOneManagementService _pingOneManagementService;
    private readonly GetChattyDataContext _dataContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="CertificationService"/> class.
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="httpClientFactory"></param>
    /// <param name="pingOneManagementService"></param>
    public CertificationService(
        ILogger<CertificationService> logger,
        IPingOneManagementService pingOneManagementService,
        GetChattyDataContext dataContext
    )
    {
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(pingOneManagementService);
        ArgumentNullException.ThrowIfNull(dataContext);

        _logger = logger;
        _pingOneManagementService = pingOneManagementService;
        _dataContext = dataContext;
    }

    /// <inheritdoc />
    public async Task ApproveAccessRequestAsync(string requestId)
    {
        var request = await _dataContext.AccessRequests.SingleOrDefaultAsync(r => r.Id.ToString() == requestId);
        var userId = request?.UserId ?? throw new NullOrEmptyTokenException("UserId is null or empty");
        var groupId = request?.GroupId ?? throw new NullOrEmptyTokenException("GroupId is null or empty");
        request.Status = AccessRequestStatus.Approved;
        await _dataContext.SaveChangesAsync();
        await _pingOneManagementService.ProvisionGroupMembershipAsync(userId, groupId);

        _logger.LogInformation("Approved access request {RequestId} for user {UserId} to join group {GroupId}", requestId, userId, groupId);
    }

    /// <inheritdoc />
    public async Task CreateAccessRequestAsync(string userId, string groupId)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(userId);
        ArgumentNullException.ThrowIfNullOrEmpty(groupId);

        var user = await _pingOneManagementService.GetUserByIdAsync(userId);
        ArgumentNullException.ThrowIfNull(user);
        var group = await _pingOneManagementService.GetGroupByIdAsync(groupId);
        ArgumentNullException.ThrowIfNull(group);

        var dbTestAccessRequest = new Data.Entities.AccessRequest
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            GroupId = groupId,
            RequestedAt = DateTime.UtcNow,
            Status = AccessRequestStatus.Pending,
            FirstName = user?.Name?.Given,
            LastName = user?.Name?.Family,
            GroupName = group?.Name
        };

        await _dataContext.AccessRequests.AddAsync(dbTestAccessRequest);
        await _dataContext.SaveChangesAsync();

        _logger.LogInformation("Created access request {RequestId} for user {UserId} to join group {GroupId}", dbTestAccessRequest.Id, userId, groupId);
    }

    /// <inheritdoc />
    public async Task DenyAccessRequestAsync(string requestId)
    {
        var request = await _dataContext.AccessRequests.SingleOrDefaultAsync(r => r.Id.ToString() == requestId);
        var userId = request?.UserId ?? throw new NullOrEmptyTokenException("UserId is null or empty");
        var groupId = request?.GroupId ?? throw new NullOrEmptyTokenException("GroupId is null or empty");
        request.Status = AccessRequestStatus.Denied;
        await _dataContext.SaveChangesAsync();

        _logger.LogInformation("Denied access request {RequestId} for user {UserId} to join group {GroupId}", requestId, userId, groupId);
    }
}