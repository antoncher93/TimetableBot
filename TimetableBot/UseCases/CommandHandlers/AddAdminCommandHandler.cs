using TimetableBot.Infrastructure;
using TimetableBot.UseCases.Adapters;
using TimetableBot.UseCases.Commands;

namespace TimetableBot.UseCases.CommandHandlers;

public class AddAdminCommandHandler : AddAdminCommand.IHandler
{
    private readonly ITelegramBotClientAdapter _adapter;
    private readonly ITokensRepository _tokensRepository;

    public AddAdminCommandHandler(
        ITelegramBotClientAdapter adapter,
        ITokensRepository tokensRepository)
    {
        _adapter = adapter;
        _tokensRepository = tokensRepository;
    }

    public async Task HandleAsync(AddAdminCommand command)
    {
        var token = Guid.NewGuid().ToString();
        
        await _tokensRepository.AddAsync(token);
        
        await _adapter.SendTokenAsync(
            chatId: command.ChatId,
            token: token);
    }
}