using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Persistance.Context;

namespace Persistance.Contexts
{
    internal class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        private readonly IConfiguration _configuration;

        public DesignTimeDbContextFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public ApplicationDbContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<ApplicationDbContext> dbContextOptionsBuilder = new();
            dbContextOptionsBuilder.UseSqlServer(_configuration.GetConnectionString("SqlServer"));
            return new(dbContextOptionsBuilder.Options); 
        }
    }
}
