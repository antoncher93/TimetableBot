using TimetableBot.Infrastructure;
using TimetableBot.UseCases.Adapters;

namespace TimetableBot.UseCases.CommandHandlers;

public class SendMessageCommandHandler : SendMessageCommand.IHandler
{
    private readonly ITelegramBotClientAdapter _adapter;
    private readonly IStudentRepository _studentRepository;

    public SendMessageCommandHandler(ITelegramBotClientAdapter adapter, IStudentRepository studentRepository)
    {
        _adapter = adapter;
        _studentRepository = studentRepository;
    }

    public async Task HandleAsync(SendMessageCommand command)
    {
        var students = _studentRepository.GetAllStudents();

        foreach (var student in students)
        {
            await _adapter.SendCopyOfMessageToAllAsync(
                fromChatId: command.FromChatId,
                messageId: command.MessageId, 
                chatId: student.ChatId);
        }
    }
}