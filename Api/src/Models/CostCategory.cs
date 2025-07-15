using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebApi.src.Models
{
  [Table("CostCategory")]
  public class CostCategory
  {
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public required string Name { get; set; }

    public List<Spending> Spendings { get; set; } = new();
    public Guid UserId { get; set; }

    [ForeignKey("UserId")]
    [JsonIgnore]
    public User? User { get; set; }
  }
}
