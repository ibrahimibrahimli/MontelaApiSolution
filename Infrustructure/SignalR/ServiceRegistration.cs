using Application.Abstractions.Hubs;
using Microsoft.Extensions.DependencyInjection;
using SignalR.HubServices;

namespace SignalR
{
    public static class ServiceRegistration
    {
        public static void AddSignalRServices(this IServiceCollection services)
        {
            services.AddTransient<IProductHubService, ProductHubService>();
            services.AddSignalR();
        }
    }
}
