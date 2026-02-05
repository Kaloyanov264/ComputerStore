namespace ComputerStore.DL
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBusinessLayer(this IServiceCollection services)
        {
            // Register your data layer services here
            services
                .AddSingleton<IComputerCrudService, ComputerCrudService>()
                .AddSingleton<ICustomerCrudService, CustomerCrudService>()
                .AddSingleton<ISellComputer, ISellComputer>();
            return services;
        }
    }
}
