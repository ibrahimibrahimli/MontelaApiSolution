using Application.Repositories;
using Persistance.Context;

namespace Persistance.Repositories.File
{
    public class FileReadRepository : ReadRepository<Domain.Entities.File>, IFileReadRepository
    {
        public FileReadRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
