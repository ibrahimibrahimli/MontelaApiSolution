using System.Linq.Expressions;

namespace Application.Repositories
{
    public interface IReadRepository<T> : IRepository<T> where T : class, new()
    {
        IQueryable<T> GetAll();
        IQueryable<T> GetWhere(Expression<Func<T, bool>> expression);
        Task<T> GetSingleAsync(Expression<Func<T, bool>> expression);
        Task<T> GetByIdAsync(string id);
    }
}
