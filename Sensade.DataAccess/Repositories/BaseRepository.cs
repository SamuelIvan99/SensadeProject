using Microsoft.Extensions.Configuration;
using Npgsql;
using Sensade.Shared.Models;

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
        connection.TypeMapper.MapEnum<Status>();
        return connection;
    }
}
