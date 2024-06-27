namespace FastScooter.API.Response;

public class ScooterResponse
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required string Brand { get; set; }
    public required string Model { get; set; }
    public required string Description { get; set; }
    public required string ImageUrl { get; set; }
    public double Price { get; set; }
}