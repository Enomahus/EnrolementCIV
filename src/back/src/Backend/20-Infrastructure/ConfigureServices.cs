using Infrastructure.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tools.Exceptions;

namespace Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureServices(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            //var url = configuration
            //    .GetSection("WasteTracking")
            //    .GetValue<string>("WasteTrackingUrl");
            //if (string.IsNullOrEmpty(url))
            //    throw new ConfigurationMissingException("Missing wasteTracking url");

            //var token = configuration
            //    .GetSection("WasteTracking")
            //    .GetValue<string>("WasteTrackingToken");
            //if (string.IsNullOrEmpty(token))
            //    throw new ConfigurationMissingException("Missing wasteTracking token");

            //services.AddScoped<IDateService, DateService>();
            //services.AddScoped<IDataIntegrationService, DataIntegrationService>();

            services.Configure<TokenConfiguration>(configuration.GetSection("JwtConfig"));
            services.Configure<PdfPrinterConfiguration>(configuration.GetSection("Service:Print"));

            return services;
        }
    }
}
