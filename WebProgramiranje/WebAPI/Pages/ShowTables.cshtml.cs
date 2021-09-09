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
 public class ShowTablesModel : PageModel
 {
  public List<Quantity> Tables { get; set; }
  public Restaurant Restaurant { get; set; }
  private RestaurantContext Context;
  public ShowTablesModel(RestaurantContext context)
  {
   Context = context;
  }
  public async Task OnGetAsync(int ID)
  {
   var restaurant = await Context.Restaurant.Include(p => p.Tables).FirstOrDefaultAsync(p => p.ID == ID);
   Restaurant = restaurant;
   Tables = new List<Quantity>();
   foreach (Table table in restaurant.Tables)
   {
    List<Quantity> quantity = await Context.Quantity.Include(p => p.Table).Include(p => p.Product).Where(p => p.Table == table & p.Quantities != 0).ToListAsync();
    if (quantity.Count != 0)
     Tables.AddRange(quantity);
   }
  }
 }
}
