using TimetableBot.Models;
using YandexCloudApiClient;
using YandexCloudApiClient.Entities;

namespace TimetableBot.Infrastructure;

public class YandexCloudDataProvider : IDataProvider
{
    private readonly IExcelFileReader _excelFileReader;
    private readonly string _folder;
    private readonly List<Course> _courses = new();

    public YandexCloudDataProvider(
        string folder,
        IExcelFileReader excelFileReader)
    {
        _folder = folder;
        _excelFileReader = excelFileReader;
    }

    public static YandexCloudDataProvider Create(
        string folder,
        IExcelFileReader excelFileReader)
    {
        var provider = new YandexCloudDataProvider(
            folder: folder,
            excelFileReader: excelFileReader);

        provider
            .ReloadAsync()
            .Wait();

        return provider;
    }

    public async Task ReloadAsync()
    {
        _courses.Clear();
        
        using var cloudApiClient = CloudApiClientFactory.Create();

        var response = await cloudApiClient.GetDiskPublicResourceAsync(
            resourcePublicKey: _folder);

        var resourceInfo = response.ResultOrException();

        foreach (var item in resourceInfo.Embedded.Items)
        {
            if (Path.GetExtension(item.Name) == ".xls") // проверям, что это файл Excel
            {
                var course = await this.GetCourseFromFileAsync(
                    client: cloudApiClient,
                    file: item);
                
                _courses.Add(course);
            }
        }
    }

    public List<Course> GetCourses() => _courses.ToList();

    private async Task<Course> GetCourseFromFileAsync(
        ICloudApiClient client,
        ResourceItem file)
    {
        var response = await client.DownloadFileAsync(file);
        var bytes = response.ResultOrException();
        return await _excelFileReader.ReadCourseDataFromBytesAsync(file.Name, bytes);
    }
}