using YandexCloudApiClient;
using YandexDisk.Client.Http;
using YandexDisk.Client.Protocol;

namespace Tests;

public class UnitTest1
{
    [Fact]
    public async Task Test1()
    {
        var folderLink = "https://disk.yandex.ru/d/48bm4CYQw5OTBw";

        var client = CloudApiClientFactory.Create();

        var response = await client.GetDiskPublicResourceAsync(
            resourcePublicKey: folderLink);
        
        Assert.True(true);
    }

    [Fact]
    public async Task Test2()
    {
        var client =
            new DiskHttpApi(
                oauthKey: "z7sJkEdnNYNZMr3Au6GFb1uCIiaw+GBekz27Pe0ArWfc+Z6gbooi8FuuiTzBRYnRq/J6bpmRyOJonT3VoXnDag==");

        var info= await client.MetaInfo.GetInfoAsync(
            request: new ResourceRequest() { Path = "/" });
        
        Assert.True(true);
    }
}