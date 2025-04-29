using Microsoft.Extensions.DependencyInjection;
using WorkFlowEngine.Shared.Cashing;

namespace WorkFlowEngine.Shared
{
    public static class DependancyInjection
    {
        public static IServiceCollection AddSharedServices(this IServiceCollection services)
        {
            services.AddSingleton<ICashService, CashService>();
            return services;
        }
    }
}
