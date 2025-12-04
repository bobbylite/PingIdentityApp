using System.Text.Json.Serialization;

namespace PingIdentityApp.Models;

/// <summary>
/// Represents a PingOne Verify transaction with web and QR URLs.
/// </summary>
public class VerifyTransactionCreatedResponse
{
    /// <summary>
    /// Hypermedia links related to the transaction.
    /// </summary>
    [JsonPropertyName("_links")]
    public Links? Links { get; set; }

    /// <summary>
    /// Unique transaction ID.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// The verify policy associated with this transaction.
    /// </summary>
    [JsonPropertyName("verifyPolicy")]
    public VerifyPolicy? VerifyPolicy { get; set; }

    /// <summary>
    /// Status of the transaction and verification steps.
    /// </summary>
    [JsonPropertyName("transactionStatus")]
    public TransactionStatusCreatedResponse? TransactionStatus { get; set; }

    /// <summary>
    /// URL for scanning QR code to start verification.
    /// </summary>
    [JsonPropertyName("qrUrl")]
    public string? QrUrl { get; set; }

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

    /// <summary>
    /// The verification requirements submitted.
    /// </summary>
    [JsonPropertyName("requirements")]
    public VerifyRequirementsCreatedResponse? Requirements { get; set; }

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
}

/// <summary>
/// Represents a single hypermedia link.
/// </summary>
public class Link
{
    [JsonPropertyName("href")]
    public string? Href { get; set; }
}

/// <summary>
/// Status of the transaction and individual verifications.
/// </summary>
public class TransactionStatusCreatedResponse
{
    /// <summary>
    /// Overall transaction status (e.g., REQUESTED, SUCCESS, FAILED).
    /// </summary>
    [JsonPropertyName("status")]
    public string? Status { get; set; }

    /// <summary>
    /// Individual verification results (e.g., LIVENESS, GOVERNMENT_ID).
    /// </summary>
    [JsonPropertyName("verificationStatus")]
    public Dictionary<string, string>? VerificationStatus { get; set; }
}

/// <summary>
/// Represents verification requirement fields for a transaction.
/// </summary>
public class VerifyRequirementsCreatedResponse
{
    [JsonPropertyName("given_name")]
    public VerifyField? GivenName { get; set; }

    [JsonPropertyName("family_name")]
    public VerifyField? FamilyName { get; set; }

    [JsonPropertyName("name")]
    public VerifyField? Name { get; set; }

    [JsonPropertyName("birth_date")]
    public VerifyField? BirthDate { get; set; }

    [JsonPropertyName("address")]
    public VerifyField? Address { get; set; }
}