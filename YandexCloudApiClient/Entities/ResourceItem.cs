using System.Text.Json.Serialization;

namespace YandexCloudApiClient.Entities;

public class ResourceItem
{
    [JsonPropertyName("name")]
    public string Name { get; set; }
    
    [JsonPropertyName("file")]
    public string? File { get; set; }
}