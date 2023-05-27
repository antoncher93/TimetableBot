using TimetableBot.Infrastructure;
using TimetableBot.UseCases.Adapters;
using TimetableBot.UseCases.Commands;

namespace TimetableBot.UseCases.CommandHandlers;

public class JoinAdminCommandHandler : JoinAdminCommand.IHandler
{
    private readonly ITelegramBotClientAdapter _adapter;
    private readonly ITokensRepository _tokensRepository;
    private readonly IStudentRepository _studentRepository;

    public JoinAdminCommandHandler(
        ITelegramBotClientAdapter adapter,
        ITokensRepository tokensRepository,
        IStudentRepository studentRepository)
    {
        _adapter = adapter;
        _tokensRepository = tokensRepository;
        _studentRepository = studentRepository;
    }
    public async Task HandleAsync(JoinAdminCommand adminCommand)
    {
        var tokenExists = await _tokensRepository.ContainsTokenAsync(adminCommand.Token);
        
        if (tokenExists)
        {
            await _tokensRepository.RemoveTokenAsync(adminCommand.Token);

            await _studentRepository.SaveStudentAsAdminAsync(adminCommand.Student);

            await _adapter.SendAdminJoinedAsync(adminCommand.Student);
        }

        await _adapter.SendCannotJoinAsAdminAsync(adminCommand.Student);
    }
}