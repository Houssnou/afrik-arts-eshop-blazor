using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Entities;
using Core.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data
{
    public class StoreDbContextSeed
    {
        public static async Task SeedAsync(StoreDbContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

                if (!context.ProductOrigins.Any())
                {
                    var originsData = File.ReadAllText(path + @"/Data/SeedData/origins.json");
                    
                    var origins = JsonSerializer.Deserialize<List<ProductOrigin>>(originsData);

                    //ef core sql server doesn't allow id field in seed data.
                    //exception: Cannot insert explicit value for identity column in table '' when IDENTITY_INSERT is set to OFF.
                    //it's too risky to remove the PK constraint from the column so i introduced the IDless seed data. see _old_****.json

                    foreach (var item in origins)
                    {
                        context.ProductOrigins.Add(item);
                    }
                  
                    await context.SaveChangesAsync();
                  
                }

                if (!context.ProductTypes.Any())
                {
                    var typesData = File.ReadAllText(path + @"/Data/SeedData/types.json");

                    var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);

                    foreach (var item in types)
                    {
                        context.ProductTypes.Add(item);
                    }
                    await context.SaveChangesAsync();
                }

                if (!context.Products.Any())
                {
                    var productsData = File.ReadAllText(path + @"/Data/SeedData/products.json");

                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                    foreach (var item in products)
                    {
                        context.Products.Add(item);
                    }
                    await context.SaveChangesAsync();
                }

                if (!context.Shippings.Any())
                {
                    var shippingsData = File.ReadAllText(path + @"/Data/SeedData/shipping.json");

                    var shippings = JsonSerializer.Deserialize<List<Shipping>>(shippingsData);

                    foreach (var item in shippings)
                    {
                        context.Shippings.Add(item);
                    }
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<StoreDbContextSeed>();
                logger.LogError(ex.Message);
            }
        }
    }
}