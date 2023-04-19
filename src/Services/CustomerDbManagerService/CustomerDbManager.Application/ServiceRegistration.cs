using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CustomerDbManager.Application
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddApplicationRegistration(this IServiceCollection services)
        {
            
            var assembly = Assembly.GetExecutingAssembly();

            //services.AddAutoMapper(assembly);
            //services.AddAutoMapper(typeof(UrlMappingProfile));

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assembly));

            return services;
        }
    }
}
