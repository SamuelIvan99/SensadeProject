namespace Sensade.Shared.Models;

public enum Status
{
    FREE,
    OCCUPIED
}

public class ParkingSpace
{
    public int Id { get; set; }

    public string Status { get; set; } = string.Empty;

    public int SpaceNo { get; set; }

    public int ParkingAreaId { get; set; }
}
