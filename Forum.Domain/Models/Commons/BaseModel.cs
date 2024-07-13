using System.ComponentModel.DataAnnotations;

namespace Forum.Domain.Models.Commons;

public class BaseModel
{
    [Key]
    public long Id { get; set; }

    public DateTime Created_at { get; set; } = DateTime.Now;
    public DateTime? Updated_at { get; set; }
    public DateTime? Deleted_at { get; set; }
    
}