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
 public class DeleteRestaurantModel : PageModel
 {
  private RestaurantContext Context;
  public DeleteRestaurantModel(RestaurantContext context)
  {
   Context = context;
  }
  public async Task OnGetAsync(int ID)
  {
   var restaurant = await Context.Restaurant.Include(p => p.Tables).Include(p => p.Products).FirstOrDefaultAsync(p => p.ID == ID);
   //Prvo brisemo stolove, i produkte
   if (restaurant.Tables.Count() != 0)
   {
    foreach (Table table in restaurant.Tables)
    {
     var quantity = await Context.Quantity.Include(p => p.Table).Where(p => p.Table == table).ToListAsync();
     if (quantity.Count() != 0)
      quantity.ForEach(e => Context.Remove(e));
     Context.Remove(table);
    }
   }
   //Brisemo i produkte
   if (restaurant.Products.Count() != 0)
   {
    foreach (Product product in restaurant.Products)
    {
     Context.Remove(product);
    }

   }
   Context.Remove(restaurant);
   await Context.SaveChangesAsync();
   //Redirects to page i want
   Response.Redirect("/ShowRestaurants");


  }
 }
}
