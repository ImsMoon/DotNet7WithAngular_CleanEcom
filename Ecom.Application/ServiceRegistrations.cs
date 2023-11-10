using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Ecom.Application.Features.Products.RepositoryContacts;
using Ecom.Application.Specifications;
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