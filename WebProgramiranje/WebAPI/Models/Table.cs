using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebAPI.Models
{
 [Table("Table")]
 public class Table
 {
  [Key]
  [Column("ID")]
  public int ID { get; set; }

  [JsonIgnore]
  [Required]
  public Restaurant Restaurant { get; set; }
 }
}