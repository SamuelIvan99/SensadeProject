using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Sensade.DataAccess.Repositories;

public class BaseRepository
{
    protected readonly string _connectionString;

    public BaseRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString(ConnectionString.Value);
    }

    public NpgsqlConnection OpenConnection()
    {
        var connection = new NpgsqlConnection(_connectionString);
        connection.Open();
        return connection;
    }
}
