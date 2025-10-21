using System.Text.Json.Serialization;

namespace PingIdentityApp.Models;

/// <summary>
/// Represents a verify transaction request in PingOne Verify.
/// </summary>
public class VerifyTransactionRequest
{
    /// <summary>
    /// The verify policy associated with this transaction.
    /// </summary>
    [JsonPropertyName("policy")]
    public VerifyPolicy? Policy { get; set; }

    /// <summary>
    /// The verification requirements for this transaction (e.g., name, birth date, address).
    /// </summary>
    [JsonPropertyName("requirements")]
    public VerifyRequirements? Requirements { get; set; }

    /// <summary>
    /// The URL to redirect the user after completing verification.
    /// </summary>
    [JsonPropertyName("return_url")]
    public string? ReturnUrl { get; set; }
}

/// <summary>
/// Represents the verify policy for a transaction.
/// </summary>
public class VerifyPolicy
{
    /// <summary>
    /// The ID of the verify policy.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }
}

/// <summary>
/// Represents the required information for verifying a user.
/// </summary>
public class VerifyRequirements
{
    /// <summary>
    /// User's given (first) name.
    /// </summary>
    [JsonPropertyName("given_name")]
    public VerifyField? GivenName { get; set; }

    /// <summary>
    /// User's family (last) name.
    /// </summary>
    [JsonPropertyName("family_name")]
    public VerifyField? FamilyName { get; set; }

    /// <summary>
    /// User's date of birth in YYYY-MM-DD format.
    /// </summary>
    [JsonPropertyName("birth_date")]
    public VerifyField? BirthDate { get; set; }

    /// <summary>
    /// User's physical address.
    /// </summary>
    [JsonPropertyName("address")]
    public VerifyField? Address { get; set; }
}

/// <summary>
/// Represents a single verification field (value only for this example).
/// </summary>
public class VerifyField
{
    /// <summary>
    /// The value of the field.
    /// </summary>
    [JsonPropertyName("value")]
    public string? Value { get; set; }
}
