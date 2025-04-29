using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WorkFlowEngine.Core
{
    public static class DependancyInjection
    {
        public static IServiceCollection AddMediatorDependancyInjection(this IServiceCollection services)
        {
            services.AddMediatR(s => s.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
