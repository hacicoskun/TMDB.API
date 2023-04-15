using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ServiceExtensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection ServiceCollectionExtension(this IServiceCollection services, params Type[] types)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddAutoMapper(types);
            return services;
        }
        
    }
}