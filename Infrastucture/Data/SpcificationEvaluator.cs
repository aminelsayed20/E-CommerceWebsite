using Core.Entities;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastucture.Data
{
	public class SpcificationEvaluator <T> where T : BaseEntity
	{
		public static IQueryable<T> GetQuery (IQueryable<T> inputQuery, ISpecification<T> spec )
		{
			var query = inputQuery;

			if (spec.Criateria != null) 
			{
				query = query.Where(spec.Criateria);  			
			}
			if (spec.OrderBy != null)
			{
				query = query.OrderBy(spec.OrderBy);
			}
			if (spec.OrderByDescending != null)
			{
				query = query.OrderByDescending(spec.OrderByDescending); 
			}
			
		    if (spec.IsPagingEnabled)
			{
				query = query.Skip(spec.Skip).Take(spec.Take); // this for Pagination 
			}
			query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));
			return query;

		}

	}
}
