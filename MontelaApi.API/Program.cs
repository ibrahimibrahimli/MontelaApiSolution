using Persistance;
using Scalar.AspNetCore;
namespace MontelaApi.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddPersistanceServices(builder.Configuration);

            builder.Services.AddControllers();
            builder.Services.AddOpenApi();




            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }


            app.MapScalarApiReference(options =>
            {
                options
                    .WithTitle("TITLE_HERE")
                    .WithDownloadButton(true)
                    .WithTheme(ScalarTheme.Purple)
                    .WithDefaultHttpClient(ScalarTarget.JavaScript, ScalarClient.Axios);
            });

            app.UseHttpsRedirection();

            app.UseAuthorization();

            builder.Services.AddEndpointsApiExplorer();


            app.MapControllers();

            app.Run();
        }
    }
}
