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
 public class AddProductsModel : PageModel
 {
  [BindProperty]
  public Product Product { get; set; }
  public List<Product> Products { get; set; }
  public Restaurant Restaurant { get; set; }
  private RestaurantContext Context;
  public AddProductsModel(RestaurantContext context)
  {
   Context = context;
  }
  public async Task OnGetAsync(int ID)
  {
   {
    var restaurant = await Context.Restaurant.Include(p => p.Products).FirstOrDefaultAsync(p => p.ID == ID);
    Restaurant = restaurant;
    Products = new List<Product>();
    Products = await Context.Product.Where(p => p.Restaurant == restaurant).ToListAsync();

   }
  }
  public async Task<IActionResult> OnPostAsync(int ID)
  {
   if (!ModelState.IsValid)
   {
    return Page();
   }
   var restaurant = await Context.Restaurant.Include(p => p.Tables).FirstOrDefaultAsync(p => p.ID == ID);
   Product.Restaurant = restaurant;
   await Context.Product.AddAsync(Product);
   foreach (Table table in restaurant.Tables)
   {
    var quantity = await Context.Quantity.Where(p =>

    p.Product == Product & p.Table == table
  ).FirstOrDefaultAsync();
    if (quantity == null)
    {
     Quantity quantity1 = new Quantity();
     quantity1.Table = table;
     quantity1.Product = Product;
     quantity1.Quantities = 0;
     await Context.Quantity.AddAsync(quantity1);
    }
    else quantity.Quantities++;
   }
   await Context.SaveChangesAsync();
   return Redirect("/AddProducts?id=" + ID);
  }
 }
}
