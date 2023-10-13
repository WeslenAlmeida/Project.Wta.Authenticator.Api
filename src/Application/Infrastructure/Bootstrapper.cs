using System.Reflection;
using CrossCutting.Configuration;
using Domain.Commands.v1.GenerateToken;
using Domain.Interfaces.v1;
using Infrastructure.Cache.v1;
using Infrastructure.Data.v1.Mongo;
using MediatR;

namespace Application.Infrastructure
{
    public class Bootstrapper
    {
          public Bootstrapper(IServiceCollection services)
        {
            InjectScoped(services);
            InjectLogger(services);
            InjectMediator(services);
            InjectAutoMapper(services);
            InjectRedisService(services);
        }

        private static void InjectScoped(IServiceCollection services)
        {
            services.AddScoped(typeof(IUserRepository), typeof(UserRepository)); 
            services.AddScoped(typeof(IRedisService), typeof(RedisService)); 
        }

        private static void InjectMediator(IServiceCollection services)
        {
            services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
        }

        private static void InjectAutoMapper(IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }

        private static void InjectRedisService(IServiceCollection services)
        {
            services.AddStackExchangeRedisCache(cache => 
            {
                cache.InstanceName = "Authentication.Api";
                cache.Configuration = AppSettings.RedisSettings.ConnectionString;
            });
        }

         private static void InjectLogger(IServiceCollection services)
        {
            services.AddLogging();
        }
    }
}