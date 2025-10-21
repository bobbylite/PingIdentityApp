using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PingIdentityApp.Data.Entities;

/// <summary>
/// Represents an access request in the system.
/// </summary>
[Table("AccessRequests")]
public class AccessRequest
{
    /// <summary>
    /// Unique identifier for the access request.
    /// </summary>
    [Key]
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier for the access request.
    /// </summary>
    public string? UserId { get; set; }

    /// <summary>
    /// Gets or sets the first name of the user making the access request.
    /// </summary>
    public string? FirstName { get; set; }

    /// <summary>
    /// Gets or sets the last name of the user making the access request.
    /// </summary>
    public string? LastName { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier for the group the user is requesting access to.
    /// </summary>
    public string? GroupId { get; set; }

    /// <summary>
    /// Gets or sets the name of the group the user is requesting access to.
    /// </summary>
    public string? GroupName { get; set; }

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
