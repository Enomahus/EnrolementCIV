using Application;
using Application.Features;
using Infrastructure;
using Infrastructure.Email;
using Infrastructure.Persistence;
using Infrastructure.Persistence.SQLServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tools;

namespace ServicesConfiguration
{
    public static class ConfigureServices
    {
        public static IServiceCollection ConfigureAllServices(
            this IServiceCollection services,
            IConfiguration config
        )
        {
            services.AddMediator();
            services.AddApplicationServices();
            services.AddApplicationFeaturesServices();
            services.AddInfrastructureServices(config);
            services.AddInfrastructurePersistenceServices(config);
            services.AddInfrastructureSQLServerServices(config);
            services.AddInfrastructureEmailServices(config);
            services.AddInfrastructureIdentityServices(config);
            services.AddToolsServices(config);

            return services;
        }
    }
}
