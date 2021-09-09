using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebAPI.Models
{
 [Table("Quantity")]
 public class Quantity
 {
  [Key]
  [Column("ID")]
  public int ID { get; set; }
  // [Required]
  [Column("Quantities")]
  public int Quantities { get; set; }

  public Table Table { get; set; }
  public Product Product { get; set; }
 }
}