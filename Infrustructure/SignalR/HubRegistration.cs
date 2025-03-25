using Microsoft.AspNetCore.Builder;
using SignalR.Hubs;

namespace SignalR
{
    public static class HubRegistration
    {
        public static void MapHubs(this WebApplication application)
        {
            application.MapHub<ProductHub>("/products-hub");
            application.MapHub<OrderHub>("/orders-hub");
        }
    }
}
