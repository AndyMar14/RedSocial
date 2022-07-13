using RedSocial.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RedSocial.Core.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Repository;
using RedSocial.Application.Interfaces.Repositories;
using RedSocial.Persistence.Repositories;

namespace RedSocial.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceInfrastructure(this IServiceCollection services,IConfiguration config)
        {
            if (config.GetValue<bool>("UseInMemoryDatabase")) 
            {
                services.AddDbContext<DbContextMarket>(options => options.UseInMemoryDatabase("DefaulConnetion"));
            }
            else
            {
                services.AddDbContext<DbContextMarket>(options =>
                    options.UseSqlServer(config.GetConnectionString("Conexion"),m=>m.MigrationsAssembly(typeof(DbContextMarket).Assembly.FullName)));
            }

            #region Repositories
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IPublicacionRepository, PublicacionRepository>();
            services.AddTransient<IAmigosRepository, AmigosRepository>();
            services.AddTransient<IComentarioRepository, ComentarioRepository>();
            #endregion
        }
    }
}
