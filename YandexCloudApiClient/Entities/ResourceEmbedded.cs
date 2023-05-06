using System.Text.Json.Serialization;

namespace YandexCloudApiClient.Entities;

public class ResourceEmbedded
{
    [JsonPropertyName("items")]
    public List<ResourceItem> Items { get; set; } = new List<ResourceItem>();
}