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
    
    
    [Theory]
    [InlineData("расписание на весенний семестр_2022-2023_очно-заочная форма обучения.xls")]
    [InlineData("расписание на весенний семестр_2022-2023-1 курс.xls")]
    [InlineData("расписание на весенний семестр_2022-2023-2 курс.xls")]
    [InlineData("расписание на весенний семестр_2022-2023-3 курс.xls")]
    [InlineData("расписание на весенний семестр_2022-2023-4 курс.xls")]
    [InlineData("расписание на весенний семестр_2022-2023-5 курс, магистратура.xls")]
    public async Task Test2(
        string fileName)
    {
        var excelFileReader = new ExcelFileReader();
        var bytes = await this.ReadLocalFileAsync($"C:/Test/{fileName}");
        var course = await excelFileReader.ReadCourseDataFromBytesAsync(fileName, bytes);
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


    private async Task<byte[]> ReadLocalFileAsync(string fullFileNane)
    {
        return await File.ReadAllBytesAsync(fullFileNane);
    }
}