using Application.Repositories;
using Domain.Entities;
using Persistance.Context;

namespace Persistance.Repositories
{
    public class MenuWriteRepository : WriteRepository<Menu>, IMenuWriteRepository
    {
        public MenuWriteRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
