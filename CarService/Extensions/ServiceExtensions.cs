using System;
using CarService.BL.Interfaces;
using CarService.BL.Kafka;
using CarService.BL.Services;
using CarService.DL.Interfaces;
using CarService.DL.Repositories.MsSQL;
using CarService.Models.Models;

namespace CarService.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            services.AddSingleton<IClientRepo, ClientRepo>();
            services.AddSingleton<ICarRepo, CarRepo>();
            services.AddSingleton<ITyreRepo, TyreRepo>();

            return services;
        }

        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<IClientService, ClientService>();
            services.AddSingleton<ITyreService, TyreService>();
            services.AddSingleton<ProducerService<int, Car>>();

            return services;
        }
    }
}
