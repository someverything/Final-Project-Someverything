using Core.Abstract;
using Core.Concrete;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DependencyResolver
{
    public static class ServiceRegistration
    {
        public static void AddCoreService(this IServiceCollection services)
        {
            services.AddScoped<ILangService, LanguageManager>();
        }
    }
}
