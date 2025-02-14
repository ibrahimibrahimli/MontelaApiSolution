using Domain.Entities.Common;

namespace Application.Repositories
{
    public interface IWriteRepository<T> : IRepository<T> where T : BaseEntity, new()
    {
        Task<bool> AddAsync(T entity);
        Task<bool> AddRangeAsync(List<T> entities);
        Task<bool> UpdateAsync(T entity);
        Task<bool> RemoveAsync(string id);
        Task<bool> Delete(string id);
        Task<int> SaveAsync();
    }
}
