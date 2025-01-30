using Application.Repositories;
using Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Persistance.Context;
using System.Linq.Expressions;

namespace Persistance.Repositories
{
    public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext _context;

        public ReadRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();

        public IQueryable<T> GetAll() => Table;

        public IQueryable<T> GetWhere(Expression<Func<T, bool>> expression)
        => Table.Where(expression);
        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> expression)
        => await Table.FirstOrDefaultAsync(expression);
        public async Task<T> GetByIdAsync(string id) 
            => await Table.FirstOrDefaultAsync(data => data.Id == Guid.Parse(id));
    }
}
