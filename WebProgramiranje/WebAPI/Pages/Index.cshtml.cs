using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebAPI.Models;

namespace WebAPI.Pages
{
 public class IndexModel : PageModel
 {
  [BindProperty]
  public Restaurant Restaurant { get; set; }
  private RestaurantContext Context;
  public IndexModel(RestaurantContext context)
  {
   Context = context;
  }

  public void OnGet()
  {
  }
  public async Task<IActionResult> OnPostAsync()
  {
   if (!ModelState.IsValid)
   {
    return Page();
   }
   var i = Restaurant.NumberOfColumns * Restaurant.NumberOfColumns;
   await Context.Restaurant.AddAsync(Restaurant);
   await Context.SaveChangesAsync();
   for (int j = 0; j < i; j++)
   {
    Table table = new Table();
    Context.Table.Add(table);
    table.Restaurant = Restaurant;
   }
   await Context.SaveChangesAsync();
   return RedirectToPage("./ShowRestaurants");
  }
 }
}
