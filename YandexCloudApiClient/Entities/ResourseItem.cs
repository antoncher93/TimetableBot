using System.Text.Json.Serialization;

namespace YandexCloudApiClient.Entities;

public class ResourseItem
{
    [JsonPropertyName("name")]
    public string Name { get; set; }
}