using TimetableBot.Infrastructure;
using TimetableBot.UseCases.Adapters;
using TimetableBot.UseCases.Commands;

namespace TimetableBot.UseCases.CommandHandlers;

public class SendMessageCommandHandler : SendMessageCommand.IHandler
{
    private readonly ITelegramBotClientAdapter _adapter;
    private readonly IStudentRepository _studentRepository;
    private readonly ICoursesRepository _coursesRepository;
    private readonly IGroupsRepository _groupsRepository;

    public SendMessageCommandHandler(
        ITelegramBotClientAdapter adapter,
        IStudentRepository studentRepository,
        ICoursesRepository coursesRepository,
        IGroupsRepository groupsRepository)
    {
        _adapter = adapter;
        _studentRepository = studentRepository;
        _coursesRepository = coursesRepository;
        _groupsRepository = groupsRepository;
    }

    public async Task HandleAsync(SendMessageCommand command)
    {
        if (command.GroupName != null)
        {
            var groupExists = CheckGroupExists(command.GroupName);

            if (!groupExists)
            {
                await _adapter.SendGroupDoesNotExistsAsync(
                    chatId: command.FromChatId,
                    groupName: command.GroupName);
                
                return;
            }
        }
        var students = await _studentRepository.GetAllStudentsAsync(
            groupName: command.GroupName);

        foreach (var student in students)
        {
            await _adapter.SendCopyOfMessageToAllAsync(
                fromChatId: command.FromChatId,
                messageId: command.MessageId, 
                chatId: student.ChatId);
        }

        await _adapter.NotifyMessageWasSentAsync(command.FromChatId);
    }

    private bool CheckGroupExists(string groupName)
    {
        var courses = _coursesRepository.GetCourses();

        for (int i = 0; i < courses.Count; i++)
        {
            var groups = _groupsRepository.GetGroups(i);
            if (groups.Any(g => g.Name == groupName))
            {
                return true;
            }
        }

        return false;
    }
}