using System.Text.Json.Serialization;

namespace YandexCloudApiClient.Entities;

public class ResourceInfo
{
    [JsonPropertyName("_embedded")]
    public ResourseEmbedded Embedded { get; set; }
}