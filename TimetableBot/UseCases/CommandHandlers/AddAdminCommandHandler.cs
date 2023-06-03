using TimetableBot.Infrastructure;
using TimetableBot.UseCases.Adapters;
using TimetableBot.UseCases.Commands;

namespace TimetableBot.UseCases.CommandHandlers;

public class AddAdminCommandHandler : AddAdminCommand.IHandler
{
    private readonly ITelegramBotClientAdapter _adapter;
    private readonly IAdminRepository _adminRepository;

    public AddAdminCommandHandler(
        ITelegramBotClientAdapter adapter,
        IAdminRepository adminRepository)
    {
        _adapter = adapter;
        _adminRepository = adminRepository;
    }

    public async Task HandleAsync(AddAdminCommand command)
    {
        var token = Guid.NewGuid().ToString();
        
        await _adminRepository.AddAsync(token);
        
        await _adapter.SendTokenAsync(
            chatId: command.ChatId,
            token: token);
    }
}