using Application.Repositories;
using Persistance.Context;

namespace Persistance.Repositories.File
{
    public class FileWriteRepository : WriteRepository<Domain.Entities.File>, IFileWriteRepository
    {
        public FileWriteRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
