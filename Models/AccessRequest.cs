namespace PingIdentityApp.Models;

public class AccessRequest
{
    /// <summary>
    /// Gets or sets the unique identifier for the access request.
    /// </summary>
    public string? Id { get; set; } = Guid.NewGuid().ToString();

    /// <summary>
    /// Gets or sets the unique identifier for the access request.
    /// </summary>
    public string? UserId { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier for the group the user is requesting access to.
    /// </summary>
    public string? GroupId { get; set; }

    /// <summary>
    /// Gets or sets the timestamp when the access request was made.
    /// </summary>
    public DateTime RequestedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the expiration timestamp for the access request.
    /// </summary>
    public DateTime ExpiresAt { get; set; } = DateTime.UtcNow.AddDays(7);

    /// <summary>
    /// Gets or sets the status of the access request.
    /// </summary>
    public string? Status { get; set; } = "Pending";
}