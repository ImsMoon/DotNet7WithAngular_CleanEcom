using System.Reflection;
using System.Text.Json;
using Ecom.Domain;
using Ecom.Domain.Entities.OrderAggregate;

namespace Ecom.Infrastructure
{
    public class StoreContextSeed
    {
        public static async Task SendAsync(StoreDbContext dbContext)
        {
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            if(!dbContext.ProductBrands.Any())
            {
                var brandsData = File.ReadAllText(path+@"/SeedData/brands.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
                dbContext.ProductBrands.AddRange(brands!);
            }
            if (!dbContext.ProductTypes.Any())
            {
                var typesData = File.ReadAllText(path + @"/SeedData/types.json");
                var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
                dbContext.ProductTypes.AddRange(types!);
            }

            if (!dbContext.Products.Any())
            {
                var productsData = File.ReadAllText(path + @"/SeedData/products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                dbContext.Products.AddRange(products!);
            }

            if (!dbContext.DeliveryMethods.Any())
            {
                var deliveryData = File.ReadAllText(path + @"/SeedData/delivery.json");
                var deliverables = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryData);
                dbContext.DeliveryMethods.AddRange(deliverables!);
            }

            if(dbContext.ChangeTracker.HasChanges()) await dbContext.SaveChangesAsync();

        }
    }
}