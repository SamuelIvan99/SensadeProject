namespace Sensade.Shared.Models;

public class ParkingArea
{
    public int Id { get; set; }

    public string Address { get; set; } = string.Empty;

    public string City { get; set; } = string.Empty;

    public string ZipCode { get; set; } = string.Empty;

    public double Latitude { get; set; }

    public double Longitude { get; set; }
}