using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastucture.Data
{
	public class StoreContextSeed
	{
		public static async Task SeedAsync(StoreContext context)
		{
			if (!context.productBrands.Any ()) 
			{
				var brandsData = File.ReadAllText("../Infrastucture/Data/SeedData/brands.json");
				
				var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
				context.productBrands.AddRange(brands);
			}
			if (!context.Products.Any())
			{
				var productsData = File.ReadAllText("../Infrastucture/Data/SeedData/products.json");
				var products = JsonSerializer.Deserialize<List<Product>>(productsData);
				context.Products.AddRange(products);
			}
			if (!context.productTypes.Any())
			{
				var typesData = File.ReadAllText("../Infrastucture/Data/SeedData/types.json");
				var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
				context.productTypes.AddRange(types);
			}

			if (context.ChangeTracker.HasChanges())
				await context.SaveChangesAsync();

		}
	}
}
