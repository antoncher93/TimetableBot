using System.Text.Json.Serialization;

namespace YandexCloudApiClient.Entities;

public class ResourseEmbedded
{
    [JsonPropertyName("items")]
    public List<ResourseItem> Items { get; set; } = new List<ResourseItem>();
}