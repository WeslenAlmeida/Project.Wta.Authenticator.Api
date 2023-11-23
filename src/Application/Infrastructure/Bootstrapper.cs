using System.Reflection;
using CrossCutting.Configuration;
using Domain.Commands.v1.GenerateToken;
using Domain.Interfaces.v1;
using Domain.Shared.v1.Validation;
using FluentValidation;
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
            InjectionTransient(services);
            InjectionValidator(services);
        }

        private static void InjectScoped(IServiceCollection services)
        {
            services.AddScoped(typeof(IUserRepository), typeof(UserRepository)); 
            services.AddScoped(typeof(IRedisService), typeof(RedisService)); 
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationRequestBehavior<,>));
            services.AddScoped<IValidator<GenerateTokenCommand>, GenerateTokenCommandValidator>();
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

        private static void InjectionTransient(IServiceCollection service)
        {
            service.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationRequestBehavior<,>));
        }

        private static void InjectionValidator(IServiceCollection service)
        {
            service.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        }

         private static void InjectLogger(IServiceCollection services)
        {
            services.AddLogging();
        }
    }
}