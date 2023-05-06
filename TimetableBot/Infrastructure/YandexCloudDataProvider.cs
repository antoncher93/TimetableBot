using System.Text.RegularExpressions;
using TimetableBot.Models;
using YandexCloudApiClient;

namespace TimetableBot.Infrastructure;

public class YandexCloudDataProvider : IDataProvider
{
    private readonly string _folder;
    private List<Course> _courses = new();

    public YandexCloudDataProvider(string folder)
    {
        _folder = folder;
    }

    public static YandexCloudDataProvider Create(
        string folder)
    {
        var provider = new YandexCloudDataProvider(
            folder: folder);

        provider
            .ReloadAsync()
            .Wait();

        return provider;
    }

    public async Task ReloadAsync()
    {
        using var cloudApiClient = CloudApiClientFactory.Create();

        var response = await cloudApiClient.GetDiskPublicResourceAsync(
            resourcePublicKey: _folder);

        _courses = response.Match(
            onSuccess: result =>
            {
                var files = result.Embedded.Items
                    .Where(item => Path.GetExtension(item.Name) == ".xls");

                return files
                    .Select(file =>
                    {
                        var fileName = Path.GetFileNameWithoutExtension(file.Name); // берем имя файла без расширения
                        var courseName = GetShortName(fileName); // укорачиваем имя (берем все, что идет за последним `-` в строке
                        return new Course(courseName);
                    })
                    .ToList();
            },
            onError: (statusCode, message) => new List<Course>());
    }

    public List<Course> GetCourses() => _courses.ToList();

    private string GetShortName(string name)
    {
        return Regex.Match(name, @"[^-]*$").Value;
    }
}