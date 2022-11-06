using Dapper;
using Microsoft.Extensions.Configuration;
using Sensade.Shared.Models;

namespace Sensade.DataAccess.Repositories;

public interface IParkingAreaRepository : IRepository<ParkingArea>
{

}

public class ParkingAreaRepository : BaseRepository, IParkingAreaRepository
{
    public ParkingAreaRepository(IConfiguration configuration) : base(configuration)
    {
    }

    public async Task<bool> Create(ParkingArea entityToCreate)
    {
        using var connection = OpenConnection();

        var parameters = new
        {
            StreetAddress = entityToCreate.StreetAddress,
            City = entityToCreate.City,
            ZipCode = entityToCreate.ZipCode,
            Latitude = entityToCreate.Latitude,
            Longitude = entityToCreate.Longitude
        };
        string query = "INSERT INTO parking_area(street_address, city, zip_code, latitude, longitude) " +
                        "VALUES (@StreetAddress, @City, @ZipCode, @Latitude, @Longitude)";

        var result = (await connection.ExecuteAsync(query, parameters)) > 0;

        return result;
    }

    public async Task<bool> Delete(int id)
    {
        // delete only if there are no parking spaces associated with parking area or cascade?
        using var connection = OpenConnection();

        var parameters = new { Id = id };
        string query = "DELETE FROM parking_area " +
                        "WHERE id=@Id";

        var result = (await connection.ExecuteAsync(query, parameters)) > 0;

        return result;
    }

    public async Task<IEnumerable<ParkingArea>> Get()
    {
        using var connection = OpenConnection();

        string query = "SELECT id, street_address StreetAddress, city, zip_code ZipCode, latitude, longitude " +
                        "FROM parking_area";

        var parkingAreas = (await connection.QueryAsync<ParkingArea>(query)).ToList();

        return parkingAreas;
    }

    public async Task<ParkingArea?> Get(int id)
    {
        using var connection = OpenConnection();

        var parameters = new { Id = id };
        string query = "SELECT id, street_address StreetAddress, city, zip_code ZipCode, latitude, longitude " +
                        "FROM parking_area" +
                        "WHERE id=@Id";
        var parkingArea = await connection.QueryFirstOrDefaultAsync<ParkingArea>(query);

        return parkingArea;
    }

    public async Task<bool> Update(ParkingArea entityToUpdate)
    {
        using var connection = OpenConnection();

        var parameters = new
        {
            Id = entityToUpdate.Id,
            StreetAddress = entityToUpdate.StreetAddress,
            City = entityToUpdate.City,
            ZipCode = entityToUpdate.ZipCode,
            Latitude = entityToUpdate.Latitude,
            Longitude = entityToUpdate.Longitude
        };
        string query = "UPDATE parking_area " +
                        "SET street_address=@StreetAddress, city=@City, zip_code=@ZipCode, latitude=@Latitude, longitude=@Longitude " +
                        "WHERE id=@Id";

        var result = (await connection.ExecuteAsync(query, parameters)) > 0;

        return result;
    }
}
