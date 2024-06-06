using System.ComponentModel.DataAnnotations;

namespace FastScooter.API.Request;

public class PaymentRequest
{
    [Required]
    public int? RentId { get; set; }
    [Required]
    public int UserId { get; set; }
}