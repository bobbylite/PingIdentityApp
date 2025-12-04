using System.Net.Mime;
using System.Text.Json;
using Microsoft.Extensions.Options;
using PingIdentityApp.Configuration;
using PingIdentityApp.Models;

namespace PingIdentityApp.Services.PingOne;

public class PingOneManagementService : IPingOneManagementService
{
    private readonly ILogger<PingOneManagementService> _logger;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IOptionsMonitor<PingOneAuthenticationOptions> _optionsMonitor;

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
        IOptionsMonitor<PingOneAuthenticationOptions> optionsMonitor)
    {
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(httpClientFactory);
        ArgumentNullException.ThrowIfNull(optionsMonitor);

        _logger = logger;
        _httpClientFactory = httpClientFactory;
        _optionsMonitor = optionsMonitor;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<PingOneUser>> GetUsersAsync()
    {
        var httpClient = _httpClientFactory.CreateClient(Constants.HttpClientNames.PingOneApi);
        var response = await httpClient.GetAsync($"environments/{_optionsMonitor.CurrentValue.EnvironmentId}/users");

        response.EnsureSuccessStatusCode();

        var deserializedUsersResponse = await response.Content.ReadFromJsonAsync<PingOneResponse>();
        if (deserializedUsersResponse is null)
        {
            _logger.LogError("Failed to deserialize users response");
            return Enumerable.Empty<PingOneUser>();
        }

        var users = deserializedUsersResponse.EmbeddedData?.Users ?? Enumerable.Empty<PingOneUser>();

        return users;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Group>> GetGroupsAsync()
    {
        var httpClient = _httpClientFactory.CreateClient(Constants.HttpClientNames.PingOneApi);
        var response = await httpClient.GetAsync($"environments/{_optionsMonitor.CurrentValue.EnvironmentId}/groups");

        response.EnsureSuccessStatusCode();

        var deserializedGroupsResponse = await response.Content.ReadFromJsonAsync<PingOneResponse>();
        if (deserializedGroupsResponse is null)
        {
            _logger.LogError("Failed to deserialize groups response");
            return Enumerable.Empty<Group>();
        }

        return deserializedGroupsResponse.EmbeddedData?.Groups ?? Enumerable.Empty<Group>();
    }

    /// <inheritdoc />
    public async Task<IEnumerable<GroupMembership>> GetUsersGroupMembershipAsync(string userId)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(userId);

        var httpClient = _httpClientFactory.CreateClient(Constants.HttpClientNames.PingOneApi);
        var response = await httpClient.GetAsync($"environments/{_optionsMonitor.CurrentValue.EnvironmentId}/users/{userId}/memberOfGroups");

        response.EnsureSuccessStatusCode();

        var deserializedGroupsResponse = await response.Content.ReadFromJsonAsync<PingOneResponse>();
        if (deserializedGroupsResponse is null)
        {
            _logger.LogError("Failed to deserialize groups response");
            return Enumerable.Empty<GroupMembership>();
        }

        var groupMemberships = deserializedGroupsResponse.EmbeddedData?.GroupMemberships ?? Enumerable.Empty<GroupMembership>();

        return groupMemberships;
    }

    /// <inheritdoc />
    public async Task ProvisionGroupMembershipAsync(string userId, string groupId)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(userId);
        ArgumentNullException.ThrowIfNullOrEmpty(groupId);

        var data = new
        {
            id = groupId
        };

        var httpClient = _httpClientFactory.CreateClient(Constants.HttpClientNames.PingOneApi);

        var json = JsonSerializer.Serialize(data);
        var content = new StringContent(json, System.Text.Encoding.UTF8, MediaTypeNames.Application.Json);

        var url = $"environments/{_optionsMonitor.CurrentValue.EnvironmentId}/users/{userId}/memberOfGroups";
        var response = await httpClient.PostAsync(url, content);
        var responseContent = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            _logger.LogError("PingOne returned {StatusCode}: {Response}", response.StatusCode, responseContent);
            throw new HttpRequestException($"PingOne returned {response.StatusCode}: {responseContent}");
        }
    }

    /// <inheritdoc />
    public async Task DeprovisionGroupMembershipAsync(string userId, string groupId)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(userId);
        ArgumentNullException.ThrowIfNullOrEmpty(groupId);


        var httpClient = _httpClientFactory.CreateClient(Constants.HttpClientNames.PingOneApi);

        var url = $"environments/{_optionsMonitor.CurrentValue.EnvironmentId}/users/{userId}/memberOfGroups/{groupId}";
        var response = await httpClient.DeleteAsync(url);
        var responseContent = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            _logger.LogError("PingOne returned {StatusCode}: {Response}", response.StatusCode, responseContent);
            throw new HttpRequestException($"PingOne returned {response.StatusCode}: {responseContent}");
        }
    }

    /// <inheritdoc />
    public async Task<PingOneUser> GetUserByIdAsync(string userId)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(userId);

        var httpClient = _httpClientFactory.CreateClient(Constants.HttpClientNames.PingOneApi);
        var response = await httpClient.GetAsync($"environments/{_optionsMonitor.CurrentValue.EnvironmentId}/users/{userId}");

        response.EnsureSuccessStatusCode();

        var deserializedUserResponse = await response.Content.ReadFromJsonAsync<PingOneUser>();
        if (deserializedUserResponse is null)
        {
            _logger.LogError("Failed to deserialize user response");
            throw new InvalidOperationException("Failed to deserialize user response");
        }

        return deserializedUserResponse;
    }

    /// <inheritdoc />
    public async Task<Group> GetGroupByIdAsync(string groupId)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(groupId);

        var httpClient = _httpClientFactory.CreateClient(Constants.HttpClientNames.PingOneApi);
        var response = await httpClient.GetAsync($"environments/{_optionsMonitor.CurrentValue.EnvironmentId}/groups/{groupId}");

        response.EnsureSuccessStatusCode();

        var deserializedGroupResponse = await response.Content.ReadFromJsonAsync<Group>();
        if (deserializedGroupResponse is null)
        {
            _logger.LogError("Failed to deserialize group response");
            throw new InvalidOperationException("Failed to deserialize group response");
        }

        return deserializedGroupResponse;
    }

    /// <inheritdoc />
    public async Task<VerifyTransactionCreatedResponse> CreateVerifyTransactionAsync(string userId, string dateOfBirth, string address)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(userId);
        ArgumentNullException.ThrowIfNullOrEmpty(dateOfBirth);
        ArgumentNullException.ThrowIfNullOrEmpty(address);

        var user = await GetUserByIdAsync(userId);
        ArgumentNullException.ThrowIfNull(user);

        var httpClient = _httpClientFactory.CreateClient(Constants.HttpClientNames.PingOneApi);

        var verifyTransactionRequest = new VerifyTransactionRequest
        {
            Policy = new VerifyPolicy
            {
                Id = "bfa02484-67b8-4ccc-9f6b-20bdc621307a"
            },
            Requirements = new VerifyRequirements
            {
                GivenName = new VerifyField { Value = user.Name?.Given },
                FamilyName = new VerifyField { Value = user.Name?.Family },
                BirthDate = new VerifyField { Value = dateOfBirth },
                Address = new VerifyField { Value = address }
            },
            ReturnUrl = "https://yourapp.com/verification-complete"
        };

        var json = JsonSerializer.Serialize(verifyTransactionRequest);
        var content = new StringContent(json, System.Text.Encoding.UTF8, MediaTypeNames.Application.Json);

        var url = $"environments/{_optionsMonitor.CurrentValue.EnvironmentId}/users/{userId}/verifyTransactions";
        var response = await httpClient.PostAsync(url, content);
        var responseContent = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            _logger.LogError("PingOne returned {StatusCode}: {Response}", response.StatusCode, responseContent);
            throw new HttpRequestException($"PingOne returned {response.StatusCode}: {responseContent}");
        }

        var deserializedTransactionResponse = JsonSerializer.Deserialize<VerifyTransactionCreatedResponse>(responseContent);

        if (deserializedTransactionResponse is null)
        {
            _logger.LogError("Failed to deserialize transaction response");
            throw new InvalidOperationException("Failed to deserialize transaction response");
        }

        return deserializedTransactionResponse;
    }

    /// <inheritdoc />
    public async Task<VerifyTransactionResponse> GetVerifyTransactionAsync(string userId, string transactionId)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(userId);
        ArgumentNullException.ThrowIfNullOrEmpty(transactionId);

        var httpClient = _httpClientFactory.CreateClient(Constants.HttpClientNames.PingOneApi);
        var response = await httpClient.GetAsync($"environments/{_optionsMonitor.CurrentValue.EnvironmentId}/users/{userId}/verifyTransactions/{transactionId}");

        response.EnsureSuccessStatusCode();

        var deserializedTransactionResponse = await response.Content.ReadFromJsonAsync<VerifyTransactionResponse>();
        if (deserializedTransactionResponse is null)
        {
            _logger.LogError("Failed to deserialize transaction response");
            throw new InvalidOperationException("Failed to deserialize transaction response");
        }

        return deserializedTransactionResponse;
    }

    /// <inheritdoc />
    public void Dispose()
    {
    }
}
