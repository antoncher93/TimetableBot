using System.Text.Json;
using RestSharp;
using YandexCloudApiClient.Entities;

namespace YandexCloudApiClient;

public class CloudApiClient : ICloudApiClient
{
    private readonly RestClient _restClient;

    public CloudApiClient(RestClient restClient)
    {
        _restClient = restClient;
    }

    public async Task<ApiCloudResponse<ResourceInfo>> GetDiskPublicResourceAsync(string resourcePublicKey)
    {
        var restRequest = new RestRequest(
            resource: "v1/disk/public/resources");

        restRequest.AddQueryParameter("public_key", resourcePublicKey);

        var response = await _restClient.GetAsync(restRequest);

        if (response.IsSuccessful)
        {
            var result = JsonSerializer.Deserialize<ResourceInfo>(response.Content!);
            return ApiCloudResponse<ResourceInfo>.FromSuccess(result!);
        }
        else
        {
            var message = response.Content;
            return ApiCloudResponse<ResourceInfo>.FromError(
                statusCode: response.StatusCode,
                message: message);
        }
    }

    public async Task<ApiCloudResponse<byte[]>> DownloadFileAsync(ResourceItem item)
    {
        using var client = new RestClient();

        var request = new RestRequest(
            resource: item.File);

        var response = await client.GetAsync(request);

        if (response.IsSuccessful)
        {
            return ApiCloudResponse<byte[]>.FromSuccess(result: response.RawBytes!);
        }
        else
        {
            return ApiCloudResponse<byte[]>.FromError(
                statusCode: response.StatusCode,
                message: response.Content);
        }
    }

    public void Dispose()
    {
        _restClient.Dispose();
    }
}