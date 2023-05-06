using TimetableBot.Infrastructure;
using YandexCloudApiClient;

namespace Tests;

public class UnitTests
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
        var excelFileReader = new ExcelFileReader();
        var bytes = await this.ReadLocalFileAsync();
        var course = await excelFileReader.ReadCourseDataFromBytesAsync(bytes);
        Assert.NotNull(course);
    }

    private async Task<byte[]> DownloadFileAsync()
    {
        var folderLink = "https://disk.yandex.ru/d/48bm4CYQw5OTBw";

        var client = CloudApiClientFactory.Create();

        var response = await client.GetDiskPublicResourceAsync(
            resourcePublicKey: folderLink);

        var resourceItem = response
            .ResultOrException()
            .Embedded
            .Items
            .FirstOrDefault();
        
        var downloadFileResponse = await client
            .DownloadFileAsync(resourceItem);

        return downloadFileResponse.ResultOrException();
    }


    private async Task<byte[]> ReadLocalFileAsync()
    {
        return await File.ReadAllBytesAsync(@"C:/test.xls");
    }
}