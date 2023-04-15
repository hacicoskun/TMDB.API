using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HC.TmdbBackgroundJob.Extensions
{
    public static class ServiceCollectionExtensions1
    {
        public static IServiceCollection AddHangfireBackgroundJobServices(this IServiceCollection services, IConfiguration configuration, params Type[] types)
        {
            services.AddAutoMapper(types);
            return services;
        }
    }
}