using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebAPI.Models
{
 [Table("Product")]
 public class Product
 {
  [Key]
  [Column("Name")]
  [MaxLength(30)]
  public string Name { get; set; }
  [Column("Price")]
  public float Price { get; set; }
  [JsonIgnore]

  public Restaurant Restaurant { get; set; }

 }
}