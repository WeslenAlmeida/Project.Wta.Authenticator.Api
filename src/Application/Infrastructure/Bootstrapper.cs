using Domain.Interfaces.v1;
using Infrastructure.Data.v1.Mongo;
using MediatR;

namespace Application.Infrastructure
{
    public class Bootstrapper
    {
          public Bootstrapper(IServiceCollection services)
        {
            InjectScoped(services);
            InjectMediator(services);
            InjectAutoMapper(services);
        }

        private static void InjectScoped(IServiceCollection services)
        {
            services.AddScoped(typeof(IUserRepository), typeof(UserRepository)); 
        }

        private static void InjectMediator(IServiceCollection services)
        {
            services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
        }

        private static void InjectAutoMapper(IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }
    }
}