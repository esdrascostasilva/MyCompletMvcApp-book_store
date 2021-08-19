using DevDe.App.Extensions;
using DevDe.Business.Interfaces;
using DevDe.Business.Notifications;
using DevDe.Business.Services;
using DevDe.Data.Context;
using DevDe.Data.Repository;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.Extensions.DependencyInjection;

namespace DevDe.App.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependecies(this IServiceCollection services)
        {
            services.AddScoped<MyDbContext>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProviderRepository, ProviderRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddSingleton<IValidationAttributeAdapterProvider, CoinValidationAttributeAdapterProvider>();

            services.AddScoped<INotifier, Notifier>();
            services.AddScoped<IProviderService, ProviderService>();
            services.AddScoped<IProductService, ProductService>();


            return services;
        }
    }
}
