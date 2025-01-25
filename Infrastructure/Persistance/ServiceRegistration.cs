using Application.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistance.Concrets;
using Persistance.Context;

namespace Persistance
{
    public static class ServiceRegistration
    {
        public static void AddPersistanceServices(this IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer("Server=LAPTOP-JBUKPKDJ;Database=myDataBase;Trusted_Connection=True"));
            services.AddSingleton<IproductService, ProductService>();
        }
    }
}
