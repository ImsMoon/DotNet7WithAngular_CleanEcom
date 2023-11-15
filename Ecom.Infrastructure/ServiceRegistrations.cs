using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecom.Application.Contracts;
using Ecom.Application.Features.Products.RepositoryContacts;
using Ecom.Application.GenericRepos;
using Ecom.Application.ServiceContacts;
using Ecom.Infrastructure.Repositories;
using Ecom.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace Ecom.Infrastructure
{
    public static class ServiceRegistrations
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration cofiguration)
        {
            Action<DbContextOptionsBuilder>? optionsAction = options =>
                            options.UseSqlite(cofiguration.GetConnectionString("DefaultConnection"));

            services.AddSingleton<IConnectionMultiplexer>(c=>{
                var configuration = ConfigurationOptions.Parse(cofiguration.GetConnectionString("Redis")!,true);
                return ConnectionMultiplexer.Connect(configuration);
            });
            services.AddScoped<IBasketRepository,BasketRepository>();
            services.AddScoped<ICleanEcomDbService, CleanEcomDbService>();
            services.AddScoped<IProductRepository,ProductRepository>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddDbContext<StoreDbContext>(optionsAction);
            return services;
        }
    }
}