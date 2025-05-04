using Application.Repositories;
using Domain.Entities;
using Persistance.Context;

namespace Persistance.Repositories
{
    public class EndpointWriteRepository : WriteRepository<Endpoint>, IEndpointWriteRepository
    {
        public EndpointWriteRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
