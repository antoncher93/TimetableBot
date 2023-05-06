using System.Data;
using System.Data.SqlClient;

namespace TimetableBot.Infrastructure;

public class SqlConnectionFactory : IDbConnectionFactory
{
    private readonly string _sqlConnectionString;

    public SqlConnectionFactory(string sqlConnectionString)
    {
        _sqlConnectionString = sqlConnectionString;
    }

    public IDbConnection Create()
    {
        var dbConnection = new SqlConnection(_sqlConnectionString);
        dbConnection.Open();
        return dbConnection;
    }
}