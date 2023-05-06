using System.Text.RegularExpressions;
using OfficeOpenXml;
using TimetableBot.Models;
using Group = TimetableBot.Models.Group;

namespace TimetableBot.Infrastructure;

public class ExcelFileReader : IExcelFileReader
{
    public Task<Course> ReadCourseDataFromBytesAsync(byte[] bytes)
    {
        using var package = new ExcelPackage(new FileInfo("C:/test.xls"));
        
        //var fileName = Path.GetFileNameWithoutExtension(package.File.Name); // берем имя файла без расширения
        //var courseName = GetShortName(fileName);    // укорачиваем имя (берем все, что идет за последним `-` в строке

        var sheet = package.Workbook.Worksheets[0];
        var cell = sheet.Cells[7, 4];

        var course = new Course(
            name: "CourseName",
            groups: new List<Group>()
            {
                new Group(
                    name: "test-group")
            });

        return Task.FromResult(course);
    }
    
    private string GetShortName(string name)
    {
        return Regex.Match(name, @"[^-]*$").Value;
    }
}