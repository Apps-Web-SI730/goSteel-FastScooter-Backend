using System.ComponentModel.DataAnnotations;

namespace FastScooter.API.Request;

public class RentRequest
{
    [Required]
    public DateTime StartDate { get; set; }
    [Required]
    public DateTime EndDate { get; set; }
    [Required]
    public double Price { get; set; }
    public int? ScooterId { get; set; }
}