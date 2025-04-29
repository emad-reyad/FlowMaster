using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using WorkFlowEngine.Application.Abstractions.ProcessManager;
using WorkFlowEngine.Application.Common;
using WorkFlowEngine.Application.Features.ProcessManager.Shared.Helper;
using WorkFlowEngine.Shared.Settings;

namespace WorkFlowEngine.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationInjection(this IServiceCollection services, ConfigurationManager configuration)
        {
            // Add AutoMapper
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            // Add Custom configuration class
            services.Configure<ClientSettings>(configuration.GetSection(ClientSettings.SectionName));
            // Add Mediator
            services.AddMediatR(configuration: config => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            // Add Fluent Validation
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddScoped<IDataCash, DataCash>();
            services.AddScoped<IInstanceAccess, InstanceAccess>();
            // Add validation middleware 
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            return services;
        }
    }
}
