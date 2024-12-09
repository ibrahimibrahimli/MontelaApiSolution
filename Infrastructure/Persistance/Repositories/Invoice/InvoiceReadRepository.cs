using Application.Repositories;
using Domain.Entities;
using Persistance.Context;

namespace Persistance.Repositories.Invoice
{
    public class InvoiceReadRepository : ReadRepository<InvoiceFile>, IInvoiceReadRepository
    {
        public InvoiceReadRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
