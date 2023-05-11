using TimetableBot.Infrastructure;
using TimetableBot.UseCases.Adapters;
using TimetableBot.UseCases.Commands;

namespace TimetableBot.UseCases.CommandHandlers;

public class JoinCommandHandler : JoinCommand.IHandler
{
    private readonly ITelegramBotClientAdapter _adapter;
    private readonly ITokensRepository _tokensRepository;

    public JoinCommandHandler(
        ITelegramBotClientAdapter adapter,
        ITokensRepository tokensRepository)
    {
        _adapter = adapter;
        _tokensRepository = tokensRepository;
    }
    public Task HandleAsync(JoinCommand command)
    {
        if (_tokensRepository.Contains(command.Token))
        {
            _tokensRepository.Remove(command.Token);

            command.Student.IsAdmin = true;

            return _adapter.SendAdminJoinedAsync(command.Student);
        }

        return _adapter.SendCannotJoinAsAdminAsync(command.Student);
    }
}