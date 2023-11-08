using Microsoft.EntityFrameworkCore;
using Ecom.Domain;
using Microsoft.EntityFrameworkCore.Design;
namespace Ecom.Infrastructure;

public class StoreDbContext : DbContext
{
    public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options)
    {
    }
    public DbSet<Product> Products { get; set; }
}

public class SQLiteDbContextFactory : IDesignTimeDbContextFactory<StoreDbContext>
{    
    StoreDbContext IDesignTimeDbContextFactory<StoreDbContext>.CreateDbContext(string[] args)
    {        
        var optionsBuilder = new DbContextOptionsBuilder<StoreDbContext>();
            optionsBuilder.UseSqlite("Data source=CleanEcom.db");

            return new StoreDbContext(optionsBuilder.Options);
    }
}
