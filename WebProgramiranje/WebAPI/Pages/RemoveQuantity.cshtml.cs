using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Pages
{
 public class RemoveQuantityModel : PageModel
 {
  public Restaurant Restaurant { get; set; }
  private RestaurantContext Context;
  public RemoveQuantityModel(RestaurantContext context)
  {
   Context = context;
  }
  public async Task OnGetAsync(int ID, string Name)
  {

   var quantity = await Context.Quantity.Include(p => p.Table).Include(p => p.Product).Where(p => p.Table.ID == ID & p.Product.Name == Name).FirstOrDefaultAsync();
   var product = await Context.Product.Include(p => p.Restaurant).Where(p => p.Name == Name).FirstOrDefaultAsync();
   //    var restaurant = await Context.Restaurant.Include(p => p.Tables).Include(p => p.Products).Where(p => p.ID == product.Restaurant.ID).FirstOrDefaultAsync();
   quantity.Quantities--;
   await Context.SaveChangesAsync();
   Response.Redirect("ShowTables?id=" + product.Restaurant.ID);
  }
 }
}
