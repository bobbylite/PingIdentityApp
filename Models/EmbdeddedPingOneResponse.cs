using System.Text.Json.Serialization;

namespace PingIdentityApp.Models;

// <summary>
// Represents the response from the groups endpoint
// </summary>
public class PingOneResponse
{

    /// <summary>
    /// Gets embedded data within the response.
    /// </summary>
    [JsonPropertyName("_embedded")]
    public required EmbeddedData EmbeddedData { get; set; }
}

public class EmbeddedData
{
    /// <summary>
    /// Gets or sets the list of groups.
    /// </summary>
    [JsonPropertyName("groups")]
    public List<Group>? Groups { get; set; }

    /// <summary>
    /// Gets or sets the list of users.
    /// </summary>
    [JsonPropertyName("users")]
    public List<PingOneUser>? Users { get; set; }

    /// <summary>
    /// Gets or sets the list of group memberships for a user.
    /// </summary>
    [JsonPropertyName("groupMemberships")]
    public List<GroupMembership>? GroupMemberships { get; set; }
}

/// <summary>
/// Represents an individual group membership in PingOne.
/// </summary>
public class GroupMembership
{
    /// <summary>
    /// Gets or sets the link relationships for this membership.
    /// </summary>
    [JsonPropertyName("_links")]
    public GroupMembershipLinks? Links { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the group.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// Gets or sets the environment that this group belongs to.
    /// </summary>
    [JsonPropertyName("environment")]
    public GroupMembershipEnvironment? Environment { get; set; }

    /// <summary>
    /// Gets or sets the display name of the group.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this group is external.
    /// </summary>
    [JsonPropertyName("isExternal")]
    public bool IsExternal { get; set; }

    /// <summary>
    /// Gets or sets the population associated with the group.
    /// </summary>
    [JsonPropertyName("population")]
    public GroupMembershipPopulation? Population { get; set; }

    /// <summary>
    /// Gets or sets the membership type (e.g., DIRECT).
    /// </summary>
    [JsonPropertyName("type")]
    public string? Type { get; set; }
}

/// <summary>
/// Represents the collection of link references for a group membership.
/// </summary>
public class GroupMembershipLinks
{
    /// <summary>
    /// Gets or sets the environment link.
    /// </summary>
    [JsonPropertyName("environment")]
    public GroupMembershipLink? Environment { get; set; }

    /// <summary>
    /// Gets or sets the self link.
    /// </summary>
    [JsonPropertyName("self")]
    public GroupMembershipLink? Self { get; set; }

    /// <summary>
    /// Gets or sets the user link.
    /// </summary>
    [JsonPropertyName("user")]
    public GroupMembershipLink? User { get; set; }

    /// <summary>
    /// Gets or sets the group link.
    /// </summary>
    [JsonPropertyName("group")]
    public GroupMembershipLink? Group { get; set; }

    /// <summary>
    /// Gets or sets the population link.
    /// </summary>
    [JsonPropertyName("population")]
    public GroupMembershipLink? Population { get; set; }
}

/// <summary>
/// Represents a single hyperlink reference.
/// </summary>
public class GroupMembershipLink
{
    /// <summary>
    /// Gets or sets the target URL of the link.
    /// </summary>
    [JsonPropertyName("href")]
    public string? Href { get; set; }
}

/// <summary>
/// Represents the environment associated with a group membership.
/// </summary>
public class GroupMembershipEnvironment
{
    /// <summary>
    /// Gets or sets the environment ID.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }
}

/// <summary>
/// Represents the population associated with a group membership.
/// </summary>
public class GroupMembershipPopulation
{
    /// <summary>
    /// Gets or sets the population ID.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }
}

/// <summary>
/// Represents a PingOne user entity.
/// </summary>
public class PingOneUser
{
    /// <summary>
    /// Hypermedia links related to the user.
    /// </summary>
    [JsonPropertyName("_links")]
    public UserLinks? Links { get; set; }

    /// <summary>
    /// The unique identifier for the user.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// Information about the environment the user belongs to.
    /// </summary>
    [JsonPropertyName("environment")]
    public EnvironmentInfo? Environment { get; set; }

    /// <summary>
    /// Information about the user's account.
    /// </summary>
    [JsonPropertyName("account")]
    public AccountInfo? Account { get; set; }

    /// <summary>
    /// The date and time the user was created.
    /// </summary>
    [JsonPropertyName("createdAt")]
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// The user's email address.
    /// </summary>
    [JsonPropertyName("email")]
    public string? Email { get; set; }

    /// <summary>
    /// Indicates whether the user account is enabled.
    /// </summary>
    [JsonPropertyName("enabled")]
    public bool Enabled { get; set; }

    /// <summary>
    /// Information about the user's identity provider.
    /// </summary>
    [JsonPropertyName("identityProvider")]
    public IdentityProvider? IdentityProvider { get; set; }

    /// <summary>
    /// Information about the user's last sign-on.
    /// </summary>
    [JsonPropertyName("lastSignOn")]
    public LastSignOnInfo? LastSignOn { get; set; }

    /// <summary>
    /// The user's lifecycle status.
    /// </summary>
    [JsonPropertyName("lifecycle")]
    public LifecycleInfo? Lifecycle { get; set; }

    /// <summary>
    /// Indicates whether multi-factor authentication (MFA) is enabled.
    /// </summary>
    [JsonPropertyName("mfaEnabled")]
    public bool MfaEnabled { get; set; }

    /// <summary>
    /// The user's name information.
    /// </summary>
    [JsonPropertyName("name")]
    public UserName? Name { get; set; }

    /// <summary>
    /// The population to which this user belongs.
    /// </summary>
    [JsonPropertyName("population")]
    public PopulationInfo? Population { get; set; }

    /// <summary>
    /// The date and time the user record was last updated.
    /// </summary>
    [JsonPropertyName("updatedAt")]
    public DateTime UpdatedAt { get; set; }

    /// <summary>
    /// The username for the user.
    /// </summary>
    [JsonPropertyName("username")]
    public string? Username { get; set; }

    /// <summary>
    /// The user verification status.
    /// </summary>
    [JsonPropertyName("verifyStatus")]
    public string? VerifyStatus { get; set; }
}

/// <summary>
/// Represents an object with link references.
/// </summary>
public class Links
{
    /// <summary>
    /// The self-referential link.
    /// </summary>
    [JsonPropertyName("self")]
    public HrefObject? Self { get; set; }
}

/// <summary>
/// Represents the user-specific hypermedia links.
/// </summary>
public class UserLinks
{
    [JsonPropertyName("password")]
    public HrefObject? Password { get; set; }

    [JsonPropertyName("password.set")]
    public HrefObject? PasswordSet { get; set; }

    [JsonPropertyName("account.sendVerificationCode")]
    public HrefObject? AccountSendVerificationCode { get; set; }

    [JsonPropertyName("linkedAccounts")]
    public HrefObject? LinkedAccounts { get; set; }

    [JsonPropertyName("self")]
    public HrefObject? Self { get; set; }

    [JsonPropertyName("password.check")]
    public HrefObject? PasswordCheck { get; set; }

    [JsonPropertyName("password.reset")]
    public HrefObject? PasswordReset { get; set; }

    [JsonPropertyName("password.recover")]
    public HrefObject? PasswordRecover { get; set; }
}

/// <summary>
/// Represents a single link object with an href property.
/// </summary>
public class HrefObject
{
    /// <summary>
    /// The hyperlink reference (URL).
    /// </summary>
    [JsonPropertyName("href")]
    public string? Href { get; set; }
}

/// <summary>
/// Represents the environment object containing an ID.
/// </summary>
public class EnvironmentInfo
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }
}

/// <summary>
/// Represents account information for a user.
/// </summary>
public class AccountInfo
{
    [JsonPropertyName("canAuthenticate")]
    public bool CanAuthenticate { get; set; }

    [JsonPropertyName("status")]
    public string? Status { get; set; }
}

/// <summary>
/// Represents the user's identity provider information.
/// </summary>
public class IdentityProvider
{
    [JsonPropertyName("type")]
    public string? Type { get; set; }
}

/// <summary>
/// Represents the last sign-on information.
/// </summary>
public class LastSignOnInfo
{
    [JsonPropertyName("at")]
    public DateTime At { get; set; }

    [JsonPropertyName("remoteIp")]
    public string? RemoteIp { get; set; }
}

/// <summary>
/// Represents the lifecycle status of the user.
/// </summary>
public class LifecycleInfo
{
    [JsonPropertyName("status")]
    public string? Status { get; set; }
}

/// <summary>
/// Represents the user's name information.
/// </summary>
public class UserName
{
    [JsonPropertyName("given")]
    public string? Given { get; set; }

    [JsonPropertyName("family")]
    public string? Family { get; set; }
}

/// <summary>
/// Represents the population to which the user belongs.
/// </summary>
public class PopulationInfo
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }
}

public class Group
{
    /// <summary>
    /// Gets or sets the unique identifier for the group.
    /// </summary>
    [JsonPropertyName("id")]
    public required string Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the group.
    /// </summary>
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    /// <summary>
    /// Gets or sets the description of the group.
    /// </summary>
    [JsonPropertyName("description")]
    public required string Description { get; set; }
}