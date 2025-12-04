using System.Text.Json.Serialization;

namespace PingIdentityApp.Models;

/// <summary>
/// Represents the response for a PingOne Verify transaction.
/// </summary>
public class VerifyTransactionResponse
{
    /// <summary>
    /// The unique ID of the verify transaction.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// The verify policy associated with this transaction.
    /// </summary>
    [JsonPropertyName("verifyPolicy")]
    public VerifyPolicyResponse? VerifyPolicy { get; set; }

    /// <summary>
    /// The current status of the transaction.
    /// </summary>
    [JsonPropertyName("transactionStatus")]
    public TransactionStatus? TransactionStatus { get; set; }

    /// <summary>
    /// A list of documents that were verified.
    /// </summary>
    [JsonPropertyName("verifiedDocuments")]
    public string? VerifiedDocuments { get; set; }

    /// <summary>
    /// The verification requirements submitted.
    /// </summary>
    [JsonPropertyName("requirements")]
    public VerifyRequirementsResponse? Requirements { get; set; }

    /// <summary>
    /// Timestamp when the transaction was created.
    /// </summary>
    [JsonPropertyName("createdAt")]
    public DateTime? CreatedAt { get; set; }

    /// <summary>
    /// Timestamp when the transaction was last updated.
    /// </summary>
    [JsonPropertyName("updatedAt")]
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Timestamp when the transaction expires.
    /// </summary>
    [JsonPropertyName("expiresAt")]
    public DateTime? ExpiresAt { get; set; }

    /// <summary>
    /// Web URL to continue verification in browser.
    /// </summary>
    [JsonPropertyName("webVerificationUrl")]
    public string? WebVerificationUrl { get; set; }

    /// <summary>
    /// Verification code shown to the user.
    /// </summary>
    [JsonPropertyName("webVerificationCode")]
    public string? WebVerificationCode { get; set; }
}

/// <summary>
/// Represents the status of a transaction.
/// </summary>
public class TransactionStatus
{
    /// <summary>
    /// Overall transaction status (e.g., SUCCESS, FAILED, PENDING).
    /// </summary>
    [JsonPropertyName("status")]
    public string? Status { get; set; }

    /// <summary>
    /// Detailed verification status per type (e.g., liveness, facial comparison, government ID).
    /// </summary>
    [JsonPropertyName("verificationStatus")]
    public Dictionary<string, string>? VerificationStatus { get; set; }
}

/// <summary>
/// Represents a verify policy.
/// </summary>
public class VerifyPolicyResponse
{
    /// <summary>
    /// The ID of the verify policy.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }
}

/// <summary>
/// Represents verification requirement fields for a transaction.
/// </summary>
public class VerifyRequirementsResponse
{
    [JsonPropertyName("given_name")]
    public VerifyFieldResponse? GivenName { get; set; }

    [JsonPropertyName("family_name")]
    public VerifyFieldResponse? FamilyName { get; set; }

    [JsonPropertyName("name")]
    public VerifyFieldResponse? Name { get; set; }

    [JsonPropertyName("birth_date")]
    public VerifyFieldResponse? BirthDate { get; set; }

    [JsonPropertyName("address")]
    public VerifyFieldResponse? Address { get; set; }
}

/// <summary>
/// Represents a single verification field (value only for simplicity).
/// </summary>
public class VerifyFieldResponse
{
    /// <summary>
    /// The value of the field.
    /// </summary>
    [JsonPropertyName("value")]
    public string? Value { get; set; }
}

