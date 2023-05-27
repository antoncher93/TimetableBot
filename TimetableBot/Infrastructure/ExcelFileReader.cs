using System.Text.RegularExpressions;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using TimetableBot.Models;
using Group = TimetableBot.Models.Group;

namespace TimetableBot.Infrastructure;

public class ExcelFileReader : IExcelFileReader
{
    public Task<Course> ReadCourseDataFromBytesAsync(string fileName, byte[] bytes)
    {
        using var stream = new MemoryStream(bytes);

        var workbook = new HSSFWorkbook(stream);
        var sheet = this.SelectFirstActiveSheet(workbook);
        var name = Path.GetFileNameWithoutExtension(fileName); // берем имя файла без расширения
        var courseName = GetShortName(name);    // укорачиваем имя (берем все, что идет за последним `-` в строке
        var groups = ReadGroups(sheet);

        var course = new Course(
            name: courseName,
            groups: groups);

        return Task.FromResult(course);
    }

    private ISheet SelectFirstActiveSheet(HSSFWorkbook workbook)
    {
        for (int i = 0; i < workbook.NumberOfSheets; i++)
        {
            var sheet = workbook.GetSheetAt(i);
            
            if (sheet.IsActive)
            {
                return sheet;
            }
        }

        throw new IndexOutOfRangeException("В файле Execel отсутствует активный лист");
    }

    private List<Group> ReadGroups(ISheet sheet)
    {
        var groups = new List<Group>();
        var row = sheet.GetRow(6);
        var groupCount = CountGroups(sheet);
        
        for (int i = 3; i < 3 + groupCount; i++)
        {
            var cell = row.Cells[i];
            var week1 = ReadWeek(sheet, i);
            var week2 = ReadWeek(sheet, i + groupCount + 3);
            
            var group = new Group(
                name: cell.ToString()!,
                week1: week1,
                week2: week2);

            groups.Add(group);
        }

        return groups;
    }

    private int CountGroups(ISheet sheet)
    {
        var count = 0;
        var row = sheet.GetRow(6);
        for (int i = 3; i < row.Cells.Count; i++)
        {
            if (row.Cells[i].ToString() == "2")
            {
                break;
            }

            count++;
        }

        return count;
    }

    private StudyWeek ReadWeek(ISheet sheet, int column)
    {
        var days = new List<StudyDay>();
        var rowOffset = 0;
        
        for (int i = 0; i < 6; i++)
        {
            var rowIndex = 7 + rowOffset;
            var day = ParseDay(sheet, column, rowIndex, out var rowsCount);
            rowOffset += rowsCount;
            days.Add(day);
        }

        return new StudyWeek(days);
    }

    private StudyDay ParseDay(
        ISheet sheet, // лист
        int columnIndex, // колонка, в которой парсим уроки
        int rowIndex, // номер ряда, с которого начинаем парсить уроки
        out int rowsCount) // чисто рядов, которое занимает день
    {
        rowsCount = CountDayRows(sheet, rowIndex);

        if (sheet.GetRow(rowIndex).Cells[columnIndex].IsMergedCell)
        {
            return ParseSpecialDay(sheet, columnIndex, rowIndex);
        }

        var dayOfWeek = sheet.GetRow(rowIndex).Cells[0].ToString()!;
        var lessons = new List<Lesson>();

        for (int i = 0; i < rowsCount; i += 2)
        {
            var row1 = sheet.GetRow(rowIndex + i);
            var row2 = sheet.GetRow(rowIndex + i + 1);

            var title = row1.Cells[columnIndex].ToString()!;
            
            if (string.IsNullOrEmpty(title))
            {
                continue;
            }
            
            var startsAtValue = row1.Cells[1].ToString();
            var endsAtValue = row2.Cells[1].ToString();
            
            var lesson = new Lesson(
                startsAt: ParseStartAt(startsAtValue!),
                endsAt: ParseEndAt(endsAtValue!),
                title: title,
                description: row2.Cells[columnIndex].ToString()!);
            
            lessons.Add(lesson);
        }

        return StudyDay.FromNormal(dayOfWeek, lessons);
    }

    private StudyDay ParseSpecialDay(ISheet sheet, int columnIndex, int rowIndex)
    {
        var row = sheet.GetRow(rowIndex);
        var dayOfWeek = row.Cells[0].ToString()!;
        var description = row.Cells[columnIndex].ToString()!;
        return StudyDay.FromSpecial(
            dayOfWeek: dayOfWeek,
            specialDescription: description);
    }

    private int CountDayRows(ISheet sheet, int dayRow)
    {
        var count = 0;
        bool stop = false;
        // движемся вниз по рядам пока значение в первом столбце ""
        while (!stop)
        {
            count += 2;
            var value = sheet.GetRow(dayRow + count).Cells[2].ToString();
            stop = value == "1" 
                   || string.IsNullOrEmpty(value)
                   || value == "пара";
        }

        return count;
    }

    // Вычитывает время начала пары из строкового значения
    // Например из значения 1045-1130 вернется время 10:45:00
    private TimeSpan ParseStartAt(string value)
    {
        var hoursStr = Regex.Match(value, @"\w+(?=..[-])").Value;
        var minutes = Regex.Match(value, @"[0-9]{2}(?=-)").Value;
        
        return new TimeSpan(
            hours: int.Parse(hoursStr),
            minutes: int.Parse(minutes),
            seconds: 0);
    }
    
    // Вычитывает время конца пары из строкового значения
    // Например из значения 1045-1130 вернется время 11:30:00
    private TimeSpan ParseEndAt(string value)
    {
        var hoursStr = Regex.Match(value, @"(?<=-)\d+(?=[0-9]{2})").Value;
        var minutes = Regex.Match(value, @"[0-9]{2}$").Value;
        
        return new TimeSpan(
            hours: int.Parse(hoursStr),
            minutes: int.Parse(minutes),
            seconds: 0);
    }

    private string GetShortName(string name)
    {
        return Regex.Match(name, @"[^-]*$").Value;
    }
}