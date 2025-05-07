using Application.Repositories;
using Domain.Entities;
using Persistance.Context;

namespace Persistance.Repositories
{
    public class MenuReadRepository : ReadRepository<Menu>, IMenuReadRepository
    {
        public MenuReadRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
