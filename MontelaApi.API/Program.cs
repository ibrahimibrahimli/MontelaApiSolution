using Application;
using Application.Validators.Products;
using FluentValidation.AspNetCore;
using Infrastructure;
using Infrastructure.Filters;
using Infrastructure.Services.Storage.Azure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.IdentityModel.Tokens;
using Persistance;
using Scalar.AspNetCore;
using Serilog;
using Serilog.Context;
using Serilog.Core;
using Serilog.Sinks.MSSqlServer;
using System.Collections.ObjectModel;
using System.Data;
using System.Text;
using MontelaApi.API.Extensions;
using SignalR;
namespace MontelaApi.API
{
    public class Program
    {
        [Obsolete]
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddPersistanceServices(builder.Configuration);
            builder.Services.AddInfrastructureServices();
            builder.Services.AddApplicationServices();
            builder.Services.AddSignalRServices();


            builder.Services.AddStorage<AzureStorage>();
            //builder.Services.AddStorage(StorageType.Local);

            builder.Services.AddControllers(options => options.Filters.Add<ValidationFilters>())
                .AddFluentValidation(configuration => configuration.RegisterValidatorsFromAssemblyContaining<ProductCreateValidator>())
                .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);

            builder.Services.AddEndpointsApiExplorer();


            //builder.Services.AddValidatorsFromAssemblyContaining<ProductCreateValidator>();
            //builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer("Admin",options =>
                {
                    options.TokenValidationParameters = new()
                    {
                        ValidateAudience = true,
                        ValidateIssuer = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidAudience = builder.Configuration["JWTToken:Audience"],
                        ValidIssuer = builder.Configuration["JWTToken:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWTToken:SecurityKey"])),
                        LifetimeValidator =( notBefore,  expires,  securityToken, tokenValidationParameters) => expires != null ? expires > DateTime.UtcNow : false,
                    };
                });

            builder.Services.AddOpenApi();

            builder.Services.AddCors(options => options.AddDefaultPolicy(
                policy =>policy.WithOrigins("http://localhost:4200", "https://localhost:4200")
                .AllowCredentials()
                .AllowAnyHeader()
                .AllowAnyMethod()
            ));

            Logger log = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File("Logs/log.txt")
                .WriteTo.MSSqlServer(builder.Configuration.GetConnectionString("SqlServer"), "Logs",
                autoCreateSqlTable: true,
                columnOptions: new ColumnOptions()
                {
                    AdditionalColumns = new Collection<SqlColumn>
                    {
                       // new SqlColumn("message", SqlDbType.NVarChar, true, 1000),
                        new SqlColumn("user_name", SqlDbType.NVarChar, true, 50),
                    }
                })
                .Enrich.FromLogContext()
                .MinimumLevel.Information()
                .CreateLogger();

            builder.Host.UseSerilog(log);

            builder.Services.AddHttpLogging(logging =>
            {
                logging.LoggingFields = HttpLoggingFields.All;
                logging.RequestHeaders.Add("sec-ch-ua");
                logging.MediaTypeOptions.AddText("application/javascript");
                logging.RequestBodyLogLimit = 4096;
                logging.ResponseBodyLogLimit = 4096;
            });


            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseSerilogRequestLogging();
            app.UseHttpLogging();

            app.MapScalarApiReference("montelaApi",options =>
            {
                options
                    .WithTitle("ScalarUi Test MontelaAPI")
                    .WithDownloadButton(true)
                    .WithTheme(ScalarTheme.Purple)
                    .WithDefaultHttpClient(ScalarTarget.JavaScript, ScalarClient.Axios);
            });

            app.ConfigureExceptionHandler(app.Services.GetRequiredService<ILogger<Program>>());
            app.UseStaticFiles();

            app.UseCors();

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.Use(async(context, next) =>
            {
                var username = context.User?.Identity?.IsAuthenticated != null || true ? context.User.Identity.Name : null;
                LogContext.PushProperty("user_name", username);
                await next();
            });


            app.MapControllers();
            app.MapHubs();

            app.Run();
        }
    }
}
