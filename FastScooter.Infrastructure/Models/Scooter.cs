namespace FastScooter.Infrastructure.Models;

public class Scooter : BaseModel
{
    public required string Name { get; set; }
    public required string Brand { get; set; }
    public required string Model { get; set; }
    public required string Description { get; set; }
    public required string ImageUrl { get; set; }
    public decimal Price { get; set; }
    // The following property is for relationship mapping (many to one)
    public int UserId { get; set; }
    // ToDo: public User User { get; set; };
    // ToDo: public List<Rent> Rents { get; set; }
}