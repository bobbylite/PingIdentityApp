namespace PingIdentityApp.Constants;

/// <summary>
/// Constants representing the status of access requests.
/// </summary>
public static class AccessRequestStatus
{
    /// <summary>
    /// The status of an access request that is pending approval.
    /// </summary>
    public const string Pending = "Pending";

    /// <summary>
    /// The status of an access request that has been approved.
    /// </summary>
    public const string Approved = "Approved";

    /// <summary>
    /// The status of an access request that has been denied.
    /// </summary>
    public const string Denied = "Denied";
}
