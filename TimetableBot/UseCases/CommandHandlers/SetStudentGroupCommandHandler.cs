using TimetableBot.UseCases.Adapters;
using TimetableBot.UseCases.Commands;

namespace TimetableBot.UseCases.CommandHandlers;

public class SetStudentGroupCommandHandler : SetStudentGroupCommand.IHandler
{
    private readonly IStudentRepository _studentRepository;
    private readonly IGroupsRepository _groupsRepository;

    public SetStudentGroupCommandHandler(
        IStudentRepository studentRepository,
        IGroupsRepository groupsRepository)
    {
        _studentRepository = studentRepository;
        _groupsRepository = groupsRepository;
    }

    public async Task HandleAsync(SetStudentGroupCommand command)
    {
        var groups = _groupsRepository.GetGroups(
            course: command.CourseIndex);

        if (command.GroupIndex < groups.Count)
        {
            var group = groups[command.GroupIndex];
            await _studentRepository.SaveStudentGroupAsync(
                student: command.Student,
                group: group);
        }
        
    }
}