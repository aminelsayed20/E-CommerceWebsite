using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
	public class ProductWithFiltersForCount :BaseSpecification<Product>
	{
		public ProductWithFiltersForCount(ProductSpecParams productParams) 
		:base
		(x=>
		   // the condition                           :      => what will sending as cratiria
		(!string.IsNullOrEmpty(productParams.Search) || x.Name.ToLower().Contains(productParams.Search)) &&
		 (!productParams.BrandId.HasValue || x.ProductBrandId == productParams.BrandId) &&
		 (!productParams.TypeId.HasValue || x.ProductTypeId == productParams.TypeId)
		)// here i send 3 cratiria to BaseSpecification ginatic class
		{ }
	}
}
