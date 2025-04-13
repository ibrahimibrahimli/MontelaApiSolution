using Application.Abstractions;
using Application.Abstractions.Services;
using Application.Abstractions.Services.Configuration;
using Application.Abstractions.Storage;
using Infrastructure.Enums;
using Infrastructure.Services;
using Infrastructure.Services.Configuration;
using Infrastructure.Services.MailServices;
using Infrastructure.Services.Storage;
using Infrastructure.Services.Storage.Azure;
using Infrastructure.Services.Storage.Local;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IStorageService, StorageService>();
            services.AddScoped<ITokenHandler, TokenHandler>();
            services.AddScoped<IMailService, MailService>();
            services.AddScoped<IAuthorizeDefinitionService, AuthorizeDefinitionService>();
        }
        public static void AddStorage<T>(this IServiceCollection services) where T : Storage,  IStorage
        {
            services.AddScoped<IStorage, T>();
        }

        //Not Suggested!!!
        public static void AddStorage(this IServiceCollection services, StorageType storageType) 
        {
            switch (storageType)
            {
                case StorageType.Local:
                    services.AddScoped<IStorage, LocalStorage>();
                    break;
                case StorageType.Azure:
                    services.AddScoped<IStorage, AzureStorage>();
                    break;
                case StorageType.AWS:

                    break;
                default:
                    services.AddScoped<IStorage, LocalStorage>();
                    break;
            }
        }
    }
}
