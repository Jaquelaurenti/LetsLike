using LetsLike.Interfaces;
using LetsLike.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LetsLike.Configurations
{
    public class Factory
    {
        public static void RegisterServices(IServiceCollection services)
        {

            // TODO adicionar as referencias de services com as referencias de interfaces
            services.AddScoped<IUsuarioService, UsuarioService>();

        }
    }
}
