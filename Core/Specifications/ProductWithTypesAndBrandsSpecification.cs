using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
	public class ProductWithTypesAndBrandsSpecification : BaseSpecification<Product>
	{
        public ProductWithTypesAndBrandsSpecification()
        {
            AddIncludes(p => p.ProductBrand);
            AddIncludes(p => p.ProductType);
        }
        public ProductWithTypesAndBrandsSpecification(int id) : base (x=>x.Id == id)
        {
			AddIncludes(p => p.ProductBrand);
			AddIncludes(p => p.ProductType);

		}
    }
}
