using System.Data;

namespace TimetableBot.Infrastructure;

public interface IDbConnectionFactory
{
    IDbConnection Create();
}