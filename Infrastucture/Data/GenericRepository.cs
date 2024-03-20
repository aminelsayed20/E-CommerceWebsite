using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastucture.Data
{
	public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
	{
		private readonly StoreContext _context;

        public GenericRepository(StoreContext context)
        {
            _context = context;
        }
        public async Task<T> GetByIdAsync(int id)
		{
			return await _context.Set<T>().FindAsync(id);
		}

		public async Task<T> GetEntityWithSpecAsync(ISpecification<T> spec)
		{
          return await applySpecification(spec).FirstOrDefaultAsync();
		}

		public async Task<IReadOnlyList<T>> ListAllAsync()
		{
			return await _context.Set<T>().ToListAsync();
		}

		public async Task<IReadOnlyList<T>> ListSpecAsync(ISpecification<T> spec)
		{
			return await applySpecification(spec).ToListAsync();
		}
		private IQueryable <T> applySpecification(ISpecification<T> spec)
		{
			return SpcificationEvaluator<T>.GetQuery(_context.Set<T>(), spec);
		}
	}
}
