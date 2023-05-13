using TimetableBot.Models;

namespace TimetableBot.Infrastructure;

public class DataProvider : IDataProvider
{
    private readonly IExcelFileReader _excelFileReader;
    private readonly string _folder;
    private readonly List<Course> _courses = new();

    public DataProvider(
        string folder,
        IExcelFileReader excelFileReader)
    {
        _folder = folder;
        _excelFileReader = excelFileReader;
    }

    public static DataProvider Create(
        string folder,
        IExcelFileReader excelFileReader)
    {
        var provider = new DataProvider(
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

        var files = Directory.GetFiles("./Data", "*.xls");

        foreach (var file in files)
        {
            var course = await this.GetCourseFromFileAsync(
                file: file);
                
            _courses.Add(course);
        }
    }

    public List<Course> GetCourses() => _courses.ToList();

    private async Task<Course> GetCourseFromFileAsync(
        string file)
    {
        var bytes = await File.ReadAllBytesAsync(file);
        return await _excelFileReader.ReadCourseDataFromBytesAsync(file, bytes);
    }
}