using System.Reflection;
using Application.Features.Security.Common;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Features
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddMediator(this IServiceCollection services)
        {
            var executingAssembly = Assembly.GetExecutingAssembly();
            services.AddMediatR(config => config.RegisterServicesFromAssembly(executingAssembly));
            services.AddValidatorsFromAssembly(executingAssembly);

            return services;
        }

        public static IServiceCollection AddApplicationFeaturesServices(
            this IServiceCollection services
        )
        {
            services.AddScoped<ITokenHelper, TokenHelper>();

            return services;
        }
    }
}
