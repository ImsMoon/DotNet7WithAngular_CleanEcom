using Microsoft.EntityFrameworkCore;
using Ecom.Domain;
using Microsoft.EntityFrameworkCore.Design;
using System.Reflection;
using Ecom.Domain.Entities.OrderAggregate;
namespace Ecom.Infrastructure;

public class StoreDbContext : DbContext
{
    public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options)
    {
    }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductBrand> ProductBrands { get; set; }
    public DbSet<ProductType> ProductTypes { get; set; }
    public DbSet<Order> Orders {get;set;}
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<DeliveryMethod> DeliveryMethods { get; set; }
    
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        // to handle sqlite decimal property type issue 
        if (Database.ProviderName == "Microsoft.EntityFrameworkCore.Sqlite")
            {
                foreach (var entityType in modelBuilder.Model.GetEntityTypes())
                {
                    var properties = entityType.ClrType.GetProperties().Where(p => p.PropertyType == typeof(decimal));

                    foreach (var property in properties)
                    {
                        modelBuilder.Entity(entityType.Name).Property(property.Name).HasConversion<double>();
                    }
                }
            }
    }
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
