﻿using YandexCloudApiClient.Entities;

namespace YandexCloudApiClient;

public interface ICloudApiClient : IDisposable
{
    Task<ApiCloudResponse<ResourceInfo>> GetDiskPublicResourceAsync(
        string resourcePublicKey);

    Task<ApiCloudResponse<byte[]>> DownloadFileAsync(
        ResourceItem item);
}