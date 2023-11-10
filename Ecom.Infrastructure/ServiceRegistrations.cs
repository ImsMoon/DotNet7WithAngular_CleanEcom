using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecom.Application.Features.Products.RepositoryContacts;
using Ecom.Application.ServiceContacts;
using Ecom.Infrastructure.Repositories;
using Ecom.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Ecom.Infrastructure
{
    public static class ServiceRegistrations
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration cofiguration)
        {
            Action<DbContextOptionsBuilder>? optionsAction = options =>
                            options.UseSqlite(cofiguration.GetConnectionString("DefaultConnection"));
            services.AddScoped<ICleanEcomDbService, CleanEcomDbService>();
            services.AddScoped<IProductRepository,ProductRepository>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddDbContext<StoreDbContext>(optionsAction);
            return services;
        }
    }
}