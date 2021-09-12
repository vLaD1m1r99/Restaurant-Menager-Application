using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Pages
{
    public class ShowRestaurantModel : PageModel
    {
        private RestaurantContext Context;
  public ShowRestaurantModel(RestaurantContext context)
  {
   Context = context;
  }
       public async Task<Restaurant> OnGetAsync(int ID)
  {
   return await Context.Restaurant.Include(p => p.Tables).Include(p => p.Products).FirstOrDefaultAsync(p => p.ID == ID);
  }
    }
}
