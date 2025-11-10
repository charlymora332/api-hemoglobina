//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Design;
//using Microsoft.Extensions.Configuration;
//using System.IO;

//namespace RetoHemoglobina.Infrastructure
//{
//    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
//    {
//        public AppDbContext CreateDbContext(string[] args)
//        {
//            // Cargar configuración de Api
//            IConfigurationRoot configuration = new ConfigurationBuilder()
//                .SetBasePath(Directory.GetCurrentDirectory())
//                .AddJsonFile(Path.Combine("..", "RetoHemoglobina.Api", "appsettings.json"))
//                .Build();

//            var builder = new DbContextOptionsBuilder<AppDbContext>();
//            var connectionString = configuration.GetConnectionString("DefaultConnection");

//            builder.UseSqlServer(connectionString);

//            return new AppDbContext(builder.Options);
//        }
//    }
//}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace RetoHemoglobina.Infrastructure
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer(
        @"Server=CARLOS;Database=RetoHemoglobinaDB;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True;"
    );

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}