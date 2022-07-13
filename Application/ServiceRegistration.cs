using Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RedSocial.Application.Interfaces.Services;
using RedSocial.Application.Services;
using RedSocial.Core.Application.Interfaces.Services;
using System.Reflection;

namespace RedSocial.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddApplicationLayer(this IServiceCollection services,IConfiguration config)
        {

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            #region Services
            services.AddTransient(typeof(IGenericService<,,>), typeof(GenericService<,,>));
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IPublicacionService, PublicacionService>();
            services.AddTransient<IAmigosService, AmigosService>();
            services.AddTransient<IComentarioService, ComentarioService>();
            #endregion
        }
    }
}
