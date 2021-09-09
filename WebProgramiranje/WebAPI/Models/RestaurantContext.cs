using Microsoft.EntityFrameworkCore;
namespace WebAPI.Models
{

 public class RestaurantContext : DbContext
 {
  public DbSet<Restaurant> Restaurant { get; set; }
  public DbSet<Table> Table { get; set; }
  public DbSet<Product> Product { get; set; }
  public DbSet<Quantity> Quantity { get; set; }
  public RestaurantContext(DbContextOptions options) : base(options)
  {

  }
 }
}