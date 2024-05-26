using System.ComponentModel.DataAnnotations;

namespace FastScooter.API.Request;

public class ScooterRequest
{
    [Required] [MaxLength(50)]
    public required string Name { get; set; }
    [Required] [MaxLength(50)]
    public required string Brand { get; set; }
    [Required] [MaxLength(50)]
    public required string Model { get; set; }
    [Required] [MaxLength(500)]
    public required string Description { get; set; }
    [Required] public required string ImageUrl { get; set; }
    [Required] public double Price { get; set; }
    // Define this: public List<RentRequest>? Rents { get; set; }
}