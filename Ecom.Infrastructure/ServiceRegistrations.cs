
using Ecom.Application.Features.Basket.Contracts;
using Ecom.Application.Features.Products.RepositoryContacts;
using Ecom.Application.GenericRepos;
using Ecom.Application.ServiceContacts;
using Ecom.Domain.Entities.Identity;
using Ecom.Infrastructure.Repositories;
using Ecom.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace Ecom.Infrastructure
{
    public static class ServiceRegistrations
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration cofiguration)
        {
            Action<DbContextOptionsBuilder>? optionsAction = options =>
                            options.UseSqlite("Data source=CleanEcom.db");

            services.AddSingleton<IConnectionMultiplexer>(c=>{
                var configuration = ConfigurationOptions.Parse(cofiguration.GetConnectionString("Redis")!,true);
                return ConnectionMultiplexer.Connect(configuration);
            });


            services.AddScoped<IBasketRepository,BasketRepository>();
            services.AddScoped<ICleanEcomDbService, CleanEcomDbService>();
            services.AddScoped<IIdentityDbService,IdentityDbService>();
            services.AddSingleton<ITokenService,TokenService>();
            services.AddScoped<IAuthService,AuthService>();
            services.AddScoped<IProductRepository,ProductRepository>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddDbContext<StoreDbContext>(optionsAction);

            services.AddIdentityServices(cofiguration);
            

            return services;
        }
    }
}