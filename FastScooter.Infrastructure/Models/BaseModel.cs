using System.ComponentModel.DataAnnotations.Schema;

namespace FastScooter.Infrastructure.Models;

public class BaseModel
{
    public int Id { get; set; }
    public bool IsActive { get; set; }
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime DateCreated { get; set; }
    public DateTime? DateUpdated { get; set; }
}