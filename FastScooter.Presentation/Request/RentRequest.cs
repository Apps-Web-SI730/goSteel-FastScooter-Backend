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
    [Required]
    public int? ScooterId { get; set; }
    [Required]
    public int UserId { get; set; }
    
}