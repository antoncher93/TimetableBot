using TimetableBot.Infrastructure;
using TimetableBot.UseCases.Adapters;
using TimetableBot.UseCases.Commands;

namespace TimetableBot.UseCases.CommandHandlers;

public class JoinAdminCommandHandler : JoinAdminCommand.IHandler
{
    private readonly ITelegramBotClientAdapter _adapter;
    private readonly IAdminRepository _adminRepository;

    public JoinAdminCommandHandler(
        ITelegramBotClientAdapter adapter,
        IAdminRepository adminRepository)
    {
        _adapter = adapter;
        _adminRepository = adminRepository;
    }
    public async Task HandleAsync(JoinAdminCommand adminCommand)
    {
        var tokenExists = await _adminRepository.ContainsFreeTokenAsync(adminCommand.Token);
        
        if (tokenExists)
        {
            await _adminRepository.UpsertAdminAsync(
                token: adminCommand.Token,
                userId: adminCommand.Student.UserId);

            await _adapter.SendAdminJoinedAsync(adminCommand.Student);
        }
        else
        {
            await _adapter.SendCannotJoinAsAdminAsync(adminCommand.Student);
        }
    }
}