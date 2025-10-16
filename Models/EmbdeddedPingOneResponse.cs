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