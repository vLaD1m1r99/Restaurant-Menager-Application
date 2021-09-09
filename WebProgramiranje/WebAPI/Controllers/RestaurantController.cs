using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebAPI.Models;

namespace WebAPI.Controllers
{
 [ApiController]
 [Route("[controller]")]
 public class RestaurantController : ControllerBase
 {
  public RestaurantContext Context { get; set; }
  public RestaurantController(RestaurantContext context)
  {
   Context = context;
  }
  [Route("GetRestaurants")]
  [HttpGet]
  public async Task<List<Restaurant>> GetRestaurants()
  {
   return await Context.Restaurant.Include(p => p.Tables).Include(p => p.Products).ToListAsync();
  }
  [Route("GetRestaurant/{ID}")]
  [HttpGet]
  public async Task<Restaurant> GetRestaurant(int ID)
  {
   var restaurant = await Context.Restaurant.Include(p => p.Tables).Include(p => p.Products).FirstOrDefaultAsync(p => p.ID == ID);
   return restaurant;
  }
  // [Route("AddRestaurant")]
  // [HttpPost]
  // public async Task AddRestaurant([FromForm] Restaurant restaurant)
  // {
  //  var i = restaurant.NumberOfColumns * restaurant.NumberOfColumns;
  //  await Context.Restaurant.AddAsync(restaurant);
  //  await Context.SaveChangesAsync();
  //  for (int j = 0; j < i; j++)
  //  {
  //   Table table = new Table();
  //   Context.Table.Add(table);
  //   table.Restaurant = restaurant;
  //  }
  //  await Context.SaveChangesAsync();
  // }
  // [Route("UpdateRestaurant/{RestaurantID}")]
  // [HttpPost]
  // public async Task UpdateRestaurant([FromForm] Restaurant restaurant1, int RestaurantID)
  // {
  //  var restaurant = await Context.Restaurant.Include(p => p.Tables).Include(p => p.Products).FirstOrDefaultAsync(p => p.ID == RestaurantID);
  //  if (restaurant.NumberOfRows != restaurant1.NumberOfRows || restaurant.NumberOfColumns != restaurant1.NumberOfColumns)
  //  {
  //   if (restaurant.Tables.Count() != 0)
  //   {
  //    foreach (Table table in restaurant.Tables)
  //    {
  //     var quantity = await Context.Quantity.Include(p => p.Table).Where(p => p.Table == table).ToListAsync();
  //     if (quantity.Count() != 0)
  //      quantity.ForEach(e => Context.Remove(e));
  //     Context.Remove(table);
  //    }
  //   }
  //   restaurant.NumberOfRows = restaurant1.NumberOfRows;
  //   restaurant.NumberOfColumns = restaurant1.NumberOfColumns;
  //   var i = restaurant.NumberOfColumns * restaurant.NumberOfColumns;
  //   for (int j = 0; j < i; j++)
  //   {
  //    Table table = new Table();
  //    await Context.Table.AddAsync(table);
  //    table.Restaurant = restaurant;
  //   }
  //   foreach (Product product in restaurant.Products)
  //   {
  //    foreach (Table table in restaurant.Tables)
  //    {
  //     var quantity = await Context.Quantity.Where(p =>

  //    p.Product == product & p.Table == table
  //  ).FirstOrDefaultAsync();
  //     if (quantity == null)
  //     {
  //      Quantity quantity1 = new Quantity();
  //      quantity1.Table = table;
  //      quantity1.Product = product;
  //      quantity1.Quantities = 0;
  //      await Context.Quantity.AddAsync(quantity1);
  //     }
  //     else quantity.Quantities++;
  //    }
  //   }


  //  }
  //  restaurant.Name = restaurant1.Name;
  //  await Context.SaveChangesAsync();
  // }
  // [Route("DeleteRestaurant/{ID}")]
  // [HttpDelete]
  // public async Task DeleteRestaurant(int ID)
  // {
  //  var restaurant = await Context.Restaurant.Include(p => p.Tables).Include(p => p.Products).FirstOrDefaultAsync(p => p.ID == ID);
  //  //Prvo brisemo stolove, i produkte
  //  if (restaurant.Tables.Count() != 0)
  //  {
  //   foreach (Table table in restaurant.Tables)
  //   {
  //    var quantity = await Context.Quantity.Include(p => p.Table).Where(p => p.Table == table).ToListAsync();
  //    if (quantity.Count() != 0)
  //     quantity.ForEach(e => Context.Remove(e));
  //    Context.Remove(table);
  //   }
  //  }
  //  if (restaurant.Products.Count() != 0)
  //  {
  //   foreach (Product product in restaurant.Products)
  //   {
  //    Context.Remove(product);
  //   }

  //  }
  //  Context.Remove(restaurant);
  //  await Context.SaveChangesAsync();

  // }
  // [Route("GetTable/{TableID}")]
  // [HttpGet]
  // public async Task<Table> GetTable(int TableID)
  // {
  //  return await Context.Table.Include.FirstOrDefaultAsync(p => p.ID == TableID);
  // }
  [Route("GetTables/{RestaurantID}")]
  [HttpGet]
  public async Task<List<Table>> GetTables(int RestaurantID)
  {
   var restaurant = await Context.Restaurant.Include(p => p.Tables).FirstOrDefaultAsync(p => p.ID == RestaurantID);

   return await Context.Table.Where(p => p.Restaurant == restaurant).ToListAsync();


  }
  // [Route("UpdateTable")]
  // [HttpPut]
  // public async Task UpdateTable([FromBody] Table table)
  // {
  //  Context.Update<Table>(table);
  //  await Context.SaveChangesAsync();
  // }
  [Route("AddProductToRestaurant")]
  [HttpPost]
  public async Task AddProductToRestaurant([FromForm] Product product, [FromForm] int RestaurantID)
  {
   var restaurant = await Context.Restaurant.Include(p => p.Tables).FirstOrDefaultAsync(p => p.ID == RestaurantID);
   product.Restaurant = restaurant;
   await Context.Product.AddAsync(product);
   foreach (Table table in restaurant.Tables)
   {
    await AddProductToTable(table.ID, product.Name);
   }
   await Context.SaveChangesAsync();
  }
  // [Route("UpdateProduct")]
  // [HttpPut]
  // public async Task UpdateProduct([FromBody] Product product)
  // {
  //  Context.Update<Product>(product);
  //  await Context.SaveChangesAsync();
  // }
  //   [Route("RemoveProductFromRestaurant/{RestaurantID},{ProductID}")]
  //   [HttpDelete]
  //   public async Task DeleteProduct(int ID, int ID)
  //   {

  //    var product = await Context.Product.FindAsync(Name);
  //    Context.Remove(product);
  //    await Context.SaveChangesAsync();
  //   }
  [Route("GetProducts/{RestaurantID}")]
  [HttpGet]
  public async Task<List<Product>> GetProducts(int RestaurantID)
  {
   var restaurant = await Context.Restaurant.Include(p => p.Products).FirstOrDefaultAsync(p => p.ID == RestaurantID);
   return await Context.Product.Include(p => p.Restaurant).Where(p => p.Restaurant == restaurant).ToListAsync();
  }
  [Route("AddProductToTable/{TableID}, {ProductName}")]
  [HttpPost]
  public async Task AddProductToTable(int TableID, string ProductName)
  {
   var table = await Context.Table.FirstOrDefaultAsync(p => p.ID == TableID);
   var product = await Context.Product.FindAsync(ProductName);
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

   await Context.SaveChangesAsync();
  }
  [Route("GetNumberOfProducts/{TableID},{ProductName}")]
  [HttpGet]
  public async Task<Quantity> GetNumberOfProducts(int TableID, string ProductName)
  {
   var table = await Context.Table.FirstOrDefaultAsync(p => p.ID == TableID);
   var product = await Context.Product.FindAsync(ProductName);
   return await Context.Quantity.Where(p =>

    p.Product == product & p.Table == table
  ).FirstOrDefaultAsync();
  }
  [Route("GetProductsFromTable/{TableID}")]
  [HttpGet]
  public async Task<List<Product>> GetProductsFromTable(int TableID)
  {
   var table = await Context.Table.FirstOrDefaultAsync(p => p.ID == TableID);

   var quantity = await Context.Quantity.Include(p => p.Table).Include(p => p.Product).Where(p => p.Table == table).ToListAsync();
   var products = new List<Product>();
   if (quantity.Count() != 0)
   {

    quantity.ForEach(e => products.Add(e.Product));
    return products;

   }
   else return products;
  }

  [Route("ClearTableOfProducts/{TableID}")]
  [HttpDelete]
  public async Task ClearTableOfProducts(int TableID)
  {
   var table = await Context.Table.FirstOrDefaultAsync(p => p.ID == TableID);

   var quantity = await Context.Quantity.Include(p => p.Table).Include(p => p.Product).Where(p => p.Table == table).ToListAsync();
   if (quantity.Count() != 0)
   {

    quantity.ForEach(e => e.Quantities = 0);
    await Context.SaveChangesAsync();
   }

  }
  [Route("GetQuantitiesFromTable/{TableID}")]
  [HttpGet]
  public async Task<List<Quantity>> GetQuantitiesFromTable(int TableID)
  {
   var table = await Context.Table.FirstOrDefaultAsync(p => p.ID == TableID);
   return await Context.Quantity.Include(p => p.Table).Include(p => p.Product).Where(p => p.Table == table).ToListAsync();
  }


 }

}
