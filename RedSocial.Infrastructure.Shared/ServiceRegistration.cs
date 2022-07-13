using Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RedSocial.Core.Application.Interfaces.Services;
using System.Reflection;
using RedSocial.Domain.Settings;
using RedSocial.Shared.Services;
using RedSocial.Application.Interfaces.Services;

namespace RedSocial.Shared
{
    public static class ServiceRegistration
    {
        public static void AddSharedInfrastructure(this IServiceCollection services,IConfiguration config)
        {
            services.Configure<MailSettings>(config.GetSection("MailSettings"));
            #region Services
            services.AddTransient<IEmailService, EmailService>();
            #endregion
        }
    }
}
