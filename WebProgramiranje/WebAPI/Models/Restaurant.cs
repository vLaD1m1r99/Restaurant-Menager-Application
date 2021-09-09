using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace WebAPI.Models
{
 [Table("Restaurant")]
 public class Restaurant
 {
  [Key]
  [Column("ID")]
  public int ID { get; set; }

  [Column("Name")]
  [MaxLength(30)]
  public string Name { get; set; }
  [Column("NumberOfRows")]
  public int NumberOfRows { get; set; }
  [Column("NumberOfColumns")]
  public int NumberOfColumns { get; set; }
  public virtual List<Table> Tables { get; set; }
  public virtual List<Product> Products { get; set; }
 }
}