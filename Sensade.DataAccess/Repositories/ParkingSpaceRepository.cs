using Dapper;
using Microsoft.Extensions.Configuration;
using Sensade.Shared.Models;

namespace Sensade.DataAccess.Repositories;

public interface IParkingSpaceRepository : IRepository<ParkingSpace>
{
    public Task<bool> Update(int id, Status status);

    public Task<int> GetTotal(int parkingAreaId);

    public Task<int> GetFree(int parkingAreaId, Status status);
}

public class ParkingSpaceRepository : BaseRepository, IParkingSpaceRepository
{
    public ParkingSpaceRepository(IConfiguration configuration) : base(configuration)
    {
    }

    public async Task<bool> Create(ParkingSpace entityToCreate)
    {
        using var connection = OpenConnection();

        var parameters = new
        {
            Status = entityToCreate.Status,
            SpaceNo = entityToCreate.SpaceNo,
            ParkingAreaId = entityToCreate.ParkingAreaId
        };
        string query = "INSERT INTO parking_space(status, space_no, parking_area_id) " +
                        "VALUES (@Status, @SpaceNo, @ParkingAreaId)";

        var result = (await connection.ExecuteAsync(query, parameters)) > 0;

        return result;
    }

    public async Task<bool> Delete(int id)
    {
        using var connection = OpenConnection();

        var parameters = new { Id = id };
        string query = "DELETE FROM parking_space " +
                        "WHERE id=@Id";

        var result = (await connection.ExecuteAsync(query, parameters)) > 0;

        return result;
    }

    public async Task<IEnumerable<ParkingSpace>> Get()
    {
        using var connection = OpenConnection();

        string query = "SELECT id, status, space_no SpaceNo, parking_area_id ParkingAreaId " +
                        "FROM parking_space";

        var parkingSpaces = (await connection.QueryAsync<ParkingSpace>(query)).ToList();

        return parkingSpaces;
    }

    public async Task<ParkingSpace?> Get(int id)
    {
        using var connection = OpenConnection();

        var parameters = new { Id = id };
        string query = "SELECT id, status, space_no SpaceNo, parking_area_id ParkingAreaId " +
                        "FROM parking_space " +
                        "WHERE id=@Id";

        var parkingSpace = await connection.QueryFirstOrDefaultAsync<ParkingSpace>(query);

        return parkingSpace;
    }

    public async Task<int> GetFree(int parkingAreaId, Status status)
    {
        using var connection = OpenConnection();

        var parameters = new
        {
            ParkingAreaId = parkingAreaId,
            Status = status,
        };
        string query = "SELECT COUNT(id) " +
                        "FROM parking_space " +
                        "WHERE parking_area=@ParkingAreaId AND status=@Status";

        int result = await connection.ExecuteScalarAsync<int>(query);

        return result;
    }

    public async Task<int> GetTotal(int parkingAreaId)
    {
        using var connection = OpenConnection();

        var parameters = new
        {
            ParkingAreaId = parkingAreaId,
        };
        string query = "SELECT COUNT(id) " +
                        "FROM parking_space " +
                        "WHERE parking_area=@ParkingAreaId";

        int result = await connection.ExecuteScalarAsync<int>(query);

        return result;
    }

    public async Task<bool> Update(ParkingSpace entityToUpdate)
    {
        // add error handling - what if we update fk_parking_area to something that doesn't exist?   
        using var connection = OpenConnection();

        var parameters = new
        {
            Id = entityToUpdate.Id,
            Status = entityToUpdate.Status,
            SpaceNo = entityToUpdate.SpaceNo,
            ParkingAreaId = entityToUpdate.ParkingAreaId
        };
        string query = "UPDATE parking_space " +
                        "SET status=@Status, space_no=@SpaceNo, parking_area_id=@ParkingAreaId " +
                        "WHERE id=@Id";

        var result = (await connection.ExecuteAsync(query, parameters)) > 0;

        return result;
    }

    public async Task<bool> Update(int id, Status status)
    {
        using var connection = OpenConnection();

        var parameters = new
        {
            Id = id,
            Status = status
        };
        string query = "UPDATE parking_space " +
                        "SET status=@Status " +
                        "WHERE id=@Id";

        var result = (await connection.ExecuteAsync(query, parameters)) > 0;

        return result;
    }
}
