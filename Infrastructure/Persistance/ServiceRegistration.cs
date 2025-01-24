using Application.Abstract;
using Microsoft.Extensions.DependencyInjection;
using Persistance.Concrets;

namespace Persistance
{
    public static class ServiceRegistration
    {
        public static void AddPersistanceServices(this IServiceCollection services)
        {
            services.AddSingleton<IproductService, ProductService>();
        }
    }
}
