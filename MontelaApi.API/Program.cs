using Application.Validators.Products;
using FluentValidation.AspNetCore;
using Infrastructure;
using Infrastructure.Enums;
using Infrastructure.Filters;
using Infrastructure.Services.Storage.Azure;
using Infrastructure.Services.Storage.Local;
using Persistance;
using Scalar.AspNetCore;
namespace MontelaApi.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            Console.WriteLine(builder.Configuration.GetConnectionString("SqlServer"));

            builder.Services.AddPersistanceServices(builder.Configuration);
            builder.Services.AddInfrastructureServices();
            Console.WriteLine(builder.Configuration["Storage : Azure"]);
            builder.Services.AddStorage<AzureStorage>();
            //builder.Services.AddStorage(StorageType.Local);

            builder.Services.AddControllers(options => options.Filters.Add<ValidationFilters>())
                .AddFluentValidation(configuration => configuration.RegisterValidatorsFromAssemblyContaining<ProductCreateValidator>())
                .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);

            builder.Services.AddEndpointsApiExplorer();


            //builder.Services.AddValidatorsFromAssemblyContaining<ProductCreateValidator>();
            //builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();

            builder.Services.AddOpenApi();

            builder.Services.AddCors(options => options.AddDefaultPolicy(
                policy =>policy.WithOrigins("http://localhost:4200", "https://localhost:4200")
                .AllowAnyHeader()
                .AllowAnyMethod()
            ));




            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }


            app.MapScalarApiReference("montelaApi",options =>
            {
                options
                    .WithTitle("ScalarUi Test MontelaAPI")
                    .WithDownloadButton(true)
                    .WithTheme(ScalarTheme.Purple)
                    .WithDefaultHttpClient(ScalarTarget.JavaScript, ScalarClient.Axios);
            });
            app.UseStaticFiles();

            app.UseCors();

            app.UseHttpsRedirection();

            app.UseAuthorization();



            app.MapControllers();

            app.Run();
        }
    }
}
