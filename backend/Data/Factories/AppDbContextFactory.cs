using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Data.Models;

namespace Data.Factories
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=ProjektSystemDB;Trusted_Connection=True;");

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
