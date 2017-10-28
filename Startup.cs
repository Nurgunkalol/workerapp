using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using WorkersApp.Data;
using Newtonsoft.Json.Serialization;
using WorkersApp.Services;
using WorkersApp.Infrastructure;
using AutoMapper;

namespace WorkersApp
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; set; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();
  
            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var connection = Configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<WorkersContext>(options =>
            options.UseSqlite(connection));

            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.ContractResolver =  new CamelCasePropertyNamesContractResolver();
            });
            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                AutoMapperConfig.Register(cfg);
            });
            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);
            services.AddScoped<IWorkerRepository, WorkerRepository>();
        }
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });
            });
        }
    }
}
