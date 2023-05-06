using RestSharp;

namespace YandexCloudApiClient;

public static class CloudApiClientFactory
{
    public static ICloudApiClient Create()
    {
        var httpClient = new HttpClient()
        {
            BaseAddress = new Uri("https://cloud-api.yandex.net"),
        };
        
        return new CloudApiClient(
            restClient: new RestClient(httpClient));
    }
}