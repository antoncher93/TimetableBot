namespace TimetableBot.Models;

// Учебный день
public class StudyDay
{
   // конструктор обычного учебного дня
   private StudyDay(
      string dayOfWeek,
      List<Lesson> lessons)
   {
      Lessons = lessons;
      DayOfWeek = dayOfWeek;
   }

   // конструктор особого дня
   private StudyDay(
      string dayOfWeek,
      string specialDescription)
   {
      DayOfWeek = dayOfWeek;
      SpecialDescription = specialDescription;
      Lessons = new List<Lesson>();
   }
   public List<Lesson> Lessons { get; }
   
   public string DayOfWeek { get; }
   
   public string? SpecialDescription { get; }

   public static StudyDay FromNormal(string dayOfWeek, List<Lesson> lessons)
   {
      return new StudyDay(
         dayOfWeek: dayOfWeek,
         lessons: lessons);
   }

   public static StudyDay FromSpecial(string dayOfWeek, string specialDescription)
   {
      return new StudyDay(
         dayOfWeek: dayOfWeek,
         specialDescription: specialDescription);
   }
}