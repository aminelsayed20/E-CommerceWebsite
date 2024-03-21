using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Infrastucture.Data
{
	public class StoreContext : DbContext
	{
        public StoreContext(DbContextOptions options) :base(options) {}

		public DbSet<Product> Products { get; set; }
		public DbSet<ProductBrand> productBrands { get; set; }
		public DbSet<ProductType> productTypes { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{

			// to update data base if there have any Migration does not updated
			base.OnModelCreating(modelBuilder);
			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());


			// to solve "order by price" problem -> sqlite does not support to orderBy the decimal value
			// so i covert all decimal value to double before orderBy

			if (Database.ProviderName == "Microsoft.EntityFrameworkCore.Sqlite")
			{
				foreach (var entityType in modelBuilder.Model.GetEntityTypes())
				{
					var properties = entityType.ClrType.GetProperties().Where(p => p.PropertyType == typeof(decimal));
					foreach (var property in properties)
					{
						modelBuilder.Entity(entityType.Name).Property(property.Name).HasConversion<double>();
					}
				}
			}

		}
	}
}
