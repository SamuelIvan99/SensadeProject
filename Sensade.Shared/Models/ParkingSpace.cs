namespace Sensade.Shared.Models;

public enum Status
{
    FREE,
    OCCUPIED
}

public class ParkingSpace
{
    public int Id { get; set; }

    public Status Status { get; set; } = Status.FREE;

    public int SpaceNo { get; set; }

    public int ParkingAreaId { get; set; }
}
