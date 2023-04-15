using HC.Api.Identity.Identity;
using HC.Shared.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HC.Api.Identity.Extensions
{
    public static class ServiceCollectionIdentityExtensions
    {

        public static IServiceCollection AddApiDbContext(this IServiceCollection services, IConfiguration configuration)
        {
           return services.AddDbContext<PostgreDbContext>
            (opt => opt.UseNpgsql(configuration.GetConnectionString("PostgresConnection"),
                x => x.MigrationsHistoryTable("__ef_migrations_history", "api")
            ).UseSnakeCaseNamingConvention());

        }

        public static IdentityBuilder AddApiIdentity(this IServiceCollection services)
        {
            var builder = services.AddIdentityCore<ApiIdentityUser>(options => {
                options.SignIn.RequireConfirmedAccount = false;
                options.SignIn.RequireConfirmedEmail = false;
            });
            builder = new IdentityBuilder(builder.UserType, typeof(ApiIdentityUserRole), builder.Services);
            return builder
                  .AddEntityFrameworkStores<PostgreDbContext>()
                  .AddDefaultTokenProviders();
        }


        public static IdentityBuilder AddApiDefaultIdentity(this IServiceCollection services)
        {
            var builder = services.AddDefaultIdentity<ApiIdentityUser>(options => {
                options.SignIn.RequireConfirmedAccount = false;
                options.SignIn.RequireConfirmedEmail = false;

            });
            builder = new IdentityBuilder(builder.UserType, typeof(ApiIdentityUserRole), builder.Services);
            return builder
                  .AddEntityFrameworkStores<PostgreDbContext>()
                  .AddDefaultTokenProviders();
        }
    }
}
