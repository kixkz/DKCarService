using System;
using CarService.BL.Interfaces;
using CarService.BL.Services;
using CarService.DL.Interfaces;
using CarService.DL.Repositories.MsSQL;

namespace CarService.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            services.AddSingleton<IClientRepo, ClientRepo>();
            services.AddSingleton<ICarRepo, CarRepo>();

            return services;
        }

        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<IClientService, ClientService>();

            return services;
        }
    }
}
