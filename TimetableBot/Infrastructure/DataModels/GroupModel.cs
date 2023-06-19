namespace TimetableBot.Infrastructure.DataModels;

public class GroupModel
{
    public GroupModel()
    {
    }
    
    public GroupModel(
        string name)
    {
        Name = name;
    }
    
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public IEnumerable<StudentModel>? Students { get; set; }
}