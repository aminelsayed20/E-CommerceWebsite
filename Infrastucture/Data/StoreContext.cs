﻿using Core.Entities;
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
			base.OnModelCreating(modelBuilder);
			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
		}
	}
}