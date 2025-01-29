using Microsoft.EntityFrameworkCore;

namespace Application.Repositories
{
    public interface IRepository<T> where T : class, new()
    {
        DbSet<T> Table { get; }
    }
}
