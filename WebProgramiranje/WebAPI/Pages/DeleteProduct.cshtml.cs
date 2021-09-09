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
 public class DeleteProductModel : PageModel
 {
  private RestaurantContext Context;
  public DeleteProductModel(RestaurantContext context)
  {
   Context = context;
  }
  public async Task OnGetAsync(int ID, string Name)
  {
   var product = await Context.Product.FindAsync(Name);
   var restaurant = await Context.Restaurant.Include(p => p.Tables).Include(p => p.Products).FirstOrDefaultAsync(p => p.ID == ID);

   foreach (Table table in restaurant.Tables)
   {
    var quantity = await Context.Quantity.Where(p =>

         p.Product == product & p.Table == table
       ).FirstOrDefaultAsync();
    if (quantity != null)
     Context.Remove(quantity);

   }
   restaurant.Products.Remove(product);
   Context.Remove(product);
   await Context.SaveChangesAsync();
   Response.Redirect("/AddProducts?id=" + ID);
  }

 }
}
