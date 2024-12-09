using Application.Repositories;
using Domain.Entities;
using Persistance.Context;

namespace Persistance.Repositories.Invoice
{
    public class InvoiceWriteRepository : WriteRepository<InvoiceFile>, IInvoiceWriteRepository
    {
        public InvoiceWriteRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
