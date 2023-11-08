using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            // Action<DbContextOptionsBuilder>? optionsAction = options =>
            //                 options.UseSqlite(cofiguration.GetConnectionString("DefaultConnection"));
            
            services.AddDbContext<StoreDbContext>();
            return services;
        }
    }
}