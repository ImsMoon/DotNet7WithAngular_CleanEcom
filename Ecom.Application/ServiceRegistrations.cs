using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Ecom.Application
{
    public static class ServiceRegistrations
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {     
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}