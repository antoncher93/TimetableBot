using TimetableBot.Infrastructure;
using TimetableBot.UseCases.Queries;

namespace TimetableBot.UseCases.QueryHandlers;

public class IsUserAdminQueryHandler : IsUserAdminQuery.IHandler
{
    private readonly IAdminRepository _adminRepository;

    public IsUserAdminQueryHandler(IAdminRepository adminRepository)
    {
        _adminRepository = adminRepository;
    }

    public Task<bool> HandleAsync(IsUserAdminQuery query)
    {
        return _adminRepository.ContainsUserAsync(
            userId: query.UserId);
    }
}