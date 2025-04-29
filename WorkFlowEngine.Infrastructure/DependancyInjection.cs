using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WorkFlowEngine.Infrastructure.Abstraction;
using WorkFlowEngine.Infrastructure.Authentication;
using WorkFlowEngine.Infrastructure.Helpers;
using WorkFlowEngine.Infrastructure.Repositories;
using WorkFlowEngine.Shared.Settings;

namespace WorkFlowEngine.Infrastructure
{
    public static class DependancyInjection
    {
        public static IServiceCollection AddInfraStructure(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddSingleton<IAdHelpper, AdHelpper>();
            services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
            services.Configure<AdSettings>(configuration.GetSection(AdSettings.SectionName));
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IProcessMangerQueryRepository, ProcessMangerQueryRepository>();
            services.AddScoped<IProcessMangerCommandReposistory, ProcessMangerCommandReposistory>();
            return services;
        }
    }
}
