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
 public class EditRestaurantModel : PageModel
 {
  private RestaurantContext Context;
  [BindProperty]
  public Restaurant Restaurant { get; set; }
  public EditRestaurantModel(RestaurantContext context)
  {
   Context = context;
  }
  public async Task OnGetAsync(int ID)
  {
   var restaurant = await Context.Restaurant.Include(p => p.Tables).Include(p => p.Products).FirstOrDefaultAsync(p => p.ID == ID);
   Restaurant = restaurant;
  }
  public async Task<IActionResult> OnPostAsync(int ID)
  {
   if (!ModelState.IsValid)
   {
    return Page();
   }
   var restaurant = await Context.Restaurant.Include(p => p.Tables).Include(p => p.Products).FirstOrDefaultAsync(p => p.ID == ID);
   if (restaurant.NumberOfRows != Restaurant.NumberOfRows || restaurant.NumberOfColumns != Restaurant.NumberOfColumns)
   {
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
    restaurant.NumberOfRows = Restaurant.NumberOfRows;
    restaurant.NumberOfColumns = Restaurant.NumberOfColumns;
    var i = restaurant.NumberOfColumns * restaurant.NumberOfColumns;
    for (int j = 0; j < i; j++)
    {
     Table table = new Table();
     await Context.Table.AddAsync(table);
     table.Restaurant = restaurant;
    }
    foreach (Product product in restaurant.Products)
    {
     foreach (Table table in restaurant.Tables)
     {
      var quantity = await Context.Quantity.Where(p =>

     p.Product == product & p.Table == table
   ).FirstOrDefaultAsync();
      if (quantity == null)
      {
       Quantity quantity1 = new Quantity();
       quantity1.Table = table;
       quantity1.Product = product;
       quantity1.Quantities = 0;
       await Context.Quantity.AddAsync(quantity1);
      }
      else quantity.Quantities++;
     }
    }


   }
   restaurant.Name = Restaurant.Name;
   await Context.SaveChangesAsync();
   return RedirectToPage("ShowRestaurants");
  }

 }
}
