using ComputerStore.BL.Interfaces;
using ComputerStore.BL.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ComputerStore.BL
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBusinessLayer(this IServiceCollection services)
        {
            // Register your data layer services here
            services
                .AddSingleton<IComputerCrudService, ComputerCrudService>()
                .AddSingleton<ICustomerCrudService, CustomerCrudService>()
                .AddSingleton<ISellComputer, SellComputer>();
            return services;
        }
    }
}
