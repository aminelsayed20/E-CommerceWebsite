﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
	public class BaseSpecification <T> : ISpecification<T>
	{
		public BaseSpecification(Expression<Func<T, bool>> criteria)
        {
			Criateria = criteria;
		}
        public BaseSpecification()
        {
            
        }

		public Expression<Func<T, bool>> Criateria { get; } 

		public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>> ();
		protected void AddIncludes( Expression<Func<T, object>> include)
		{
			Includes.Add(include);
		}
	}
}
