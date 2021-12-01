using LetsLike.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LetsLike.Configurations;
using Microsoft.AspNetCore.Http;
using System.Reflection;
using System.IO;
using System.Text.Json.Serialization;

namespace LetsLike
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // TODO - Setar o nosso contexto quando a aplicação for ao ar 
            services.AddDbContext<LetsLikeContext>(
              options => options.
              UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // TODO adicionar o contexto ao escopo inicial
            services.AddDbContext<LetsLikeContext>();

            //TODO indicando acessos ao HTTP Context para trabalhar com os retornos http
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // TODO setando o mapper da aplicação para indicar que vamos trabalhar com Automapper
            services.AddAutoMapper(typeof(Startup));

            // TODO adicionando o json, para serializar as AMARRAÇÕES entre as entidades
            // relacionamentos de FK'S 
            services.AddControllers().AddJsonOptions(x =>
            x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Gerador de Like de Projetos", Version = "v1" });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            // TODO adicionando o CORS 
            /*
             *  AllowAnyOrigin: Recebe requisições de qualquer origem, caso você queira receber requisições de uma origem especifica, é somente passar a URL da origem no método WithOrigins
                AllowAnyMethod: Recebe requisições de qualquer método ex: POST, PUT, DELETE, GET e etc. Também pode restringir somente para métodos específicos, utilizando o método WithMethods
                AllowAnyHeader: Recebe requisições com qualquer tipo de cabeçalho ex: Cache-Control, Content-Language. Também pode restringir somente cabeçalhos específicos, utilizando o método WithHeader
                AllowCredentials: Recebe requisições com qualquer tipo de credencial entre origens, no cabeçalho do tipo: Access-Control-Allow-Credentials
             * */

            services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicyLetsCode", builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    /*.SetIsOriginAllowed(origin => true) // allow any origin
                        .AllowCredentials()*/
                    );
            });

            // TODO adicionando a Inversão de controle criada na Classe Factory
            RegisterServicesPrivate(services);

        }    

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "LetsLike v1"));
            }
            
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            // TODO adicionando o CORS especificando a política que criamos nas services
            // TODO obrigatoriamente precisa estar entre o useRouting e o useEndpoints
            // UseRouting Adiciona correspondência de rota ao pipeline de middleware.Esse middleware analisa o conjunto de pontos de extremidade definidos no aplicativo e seleciona a melhor correspondência com base na solicitação.
            // )UseEndpoints Adiciona a execução de ponto de extremidade ao pipeline de middleware. Ele executa o delegado associado ao ponto de extremidade selecionado.
            app.UseCors("CorsPolicyLetsCode");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        // TODO método criado para instanciar a factory
        private void RegisterServicesPrivate(IServiceCollection services)
        {
            Factory.RegisterServices(services);
        }

    }
}
