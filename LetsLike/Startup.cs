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
            // TODO - Setar o nosso contexto quando a aplica��o for ao ar 
            services.AddDbContext<LetsLikeContext>(
              options => options.
              UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // TODO adicionar o contexto ao escopo inicial
            services.AddDbContext<LetsLikeContext>();

            //TODO indicando acessos ao HTTP Context para trabalhar com os retornos http
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // TODO setando o mapper da aplica��o para indicar que vamos trabalhar com Automapper
            services.AddAutoMapper(typeof(Startup));

            // TODO adicionando o json, para serializar as AMARRA��ES entre as entidades
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
             *  AllowAnyOrigin: Recebe requisi��es de qualquer origem, caso voc� queira receber requisi��es de uma origem especifica, � somente passar a URL da origem no m�todo WithOrigins
                AllowAnyMethod: Recebe requisi��es de qualquer m�todo ex: POST, PUT, DELETE, GET e etc. Tamb�m pode restringir somente para m�todos espec�ficos, utilizando o m�todo WithMethods
                AllowAnyHeader: Recebe requisi��es com qualquer tipo de cabe�alho ex: Cache-Control, Content-Language. Tamb�m pode restringir somente cabe�alhos espec�ficos, utilizando o m�todo WithHeader
                AllowCredentials: Recebe requisi��es com qualquer tipo de credencial entre origens, no cabe�alho do tipo: Access-Control-Allow-Credentials
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

            // TODO adicionando a Invers�o de controle criada na Classe Factory
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
            // TODO adicionando o CORS especificando a pol�tica que criamos nas services
            // TODO obrigatoriamente precisa estar entre o useRouting e o useEndpoints
            // UseRouting Adiciona correspond�ncia de rota ao pipeline de middleware.Esse middleware analisa o conjunto de pontos de extremidade definidos no aplicativo e seleciona a melhor correspond�ncia com base na solicita��o.
            // )UseEndpoints Adiciona a execu��o de ponto de extremidade ao pipeline de middleware. Ele executa o delegado associado ao ponto de extremidade selecionado.
            app.UseCors("CorsPolicyLetsCode");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        // TODO m�todo criado para instanciar a factory
        private void RegisterServicesPrivate(IServiceCollection services)
        {
            Factory.RegisterServices(services);
        }

    }
}
