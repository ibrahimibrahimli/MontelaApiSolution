namespace Application.Repositories
{
    public interface IWriteRepository<T> : IRepository<T> where T : class, new()
    {
        Task<bool> AddAsync(T entity);
        Task<bool> AddRangeAsync(List<T> entities);
        Task<bool> UpdateAsync(T entity);
        Task<bool> Remove(string id);
        Task<bool> Delete(string id);
    }
}
