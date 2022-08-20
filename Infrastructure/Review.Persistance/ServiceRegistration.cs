using Microsoft.Extensions.DependencyInjection;
using Review.Persistance.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Review.Persistance
{
    public static class ServiceRegistration // static because of extension function 
    {
        public static void AddPersistanceServices(this IServiceCollection services)
        {
            services.AddSingleton<ReviewService>();
        }
    }
}
