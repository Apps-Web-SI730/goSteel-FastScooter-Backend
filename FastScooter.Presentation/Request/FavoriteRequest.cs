using System.ComponentModel.DataAnnotations;

namespace FastScooter.API.Request;

public class FavoriteRequest
{
    [Required]
    public int? ScooterId { get; set; }
    [Required]
    public int UserId { get; set; }
}