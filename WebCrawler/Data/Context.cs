using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Options;
using WebCrawler.Model;

namespace WebCrawler.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
            var created = this.Database.EnsureCreated();

            if (!created)
            {
                var databaseCreator = (this.Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator);
                databaseCreator.CreateTables();
            }
        }
        public  DbSet<Location> locations { get; set; }
        public DbSet<Current> weathers { get; set; }

    }
}
