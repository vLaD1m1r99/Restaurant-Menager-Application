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
 public class ShowRestaurantsModel : PageModel
 {
  public List<Restaurant> Restaurants { get; set; }
  private RestaurantContext Context;
  public ShowRestaurantsModel(RestaurantContext context)
  {
   Context = context;
  }
  public async Task OnGetAsync()
  {
   Restaurants = await Context.Restaurant.ToListAsync();
  }
 }
}
