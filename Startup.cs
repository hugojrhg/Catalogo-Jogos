using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ApiCatalogoJogos.Services;
using ApiCatalogoJogos.Repositories;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;
using System.Reflection;






namespace ApiCatalogoJogos
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
            services.AddRazorPages();

            services.AddScoped<IJogoService, JogoService>();
            services.AddScoped<IJogoRepository, JogoRepository>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new Info { Title = "ApiCatalogoJogos", Version = "v1"});
                    var basePath = AppDomain.CurrentDomain.BaseDirectory;
                    var fileName = typeof(Startup).GetTypeInfo().Assembly.GetName().Name + ".xml";
                    c.IncludeXmlComments(Path.Combine(basePath, fileName));
                });
            
            services.AddSwaggerDocument();
            services.AddMvcCore().AddApiExplorer();
            
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseOpenApi();
                
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiCatalogoJogos v1"));
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

        

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
