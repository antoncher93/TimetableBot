using TimetableBot.Infrastructure;
using TimetableBot.UseCases.Adapters;
using TimetableBot.UseCases.Commands;

namespace TimetableBot.UseCases.CommandHandlers;

public class DeleteAdminCommandHandler : DeleteAdminCommand.IHandler
{
    private readonly IAdminRepository _adminRepository;
    private readonly ITelegramBotClientAdapter _clientAdapter;

    public DeleteAdminCommandHandler(IAdminRepository adminRepository, ITelegramBotClientAdapter clientAdapter)
    {
        _adminRepository = adminRepository;
        _clientAdapter = clientAdapter;
    }

    public async Task HandleAsync(DeleteAdminCommand command)
    {
        await _adminRepository.RemoveTokenAsync(
            token: command.Token);

        await _clientAdapter.SendAdminHasBeenDeletedAsync(command.ChatId);
    }
}