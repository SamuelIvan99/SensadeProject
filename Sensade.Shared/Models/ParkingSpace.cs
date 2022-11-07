using NpgsqlTypes;

namespace Sensade.Shared.Models;

public enum Status
{
    [PgName("FREE")]
    FREE,
    [PgName("OCCUPIED")]
    OCCUPIED
}

public class ParkingSpace
{
    public int Id { get; set; }

    public string Status { get; set; } = string.Empty;

    public int SpaceNo { get; set; }

    public int ParkingAreaId { get; set; }
}
