using DBLayer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CoreRestAPI
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

            //AddAutoMapper will scan all the profiles which contains mapping functionality
            //AppDomain.CurrentDomain.GetAssemblies() loads all the profiles in current domain [check profile folder].

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


            services.AddControllers(setupAction =>
            {
                setupAction.ReturnHttpNotAcceptable = true;
            }).AddXmlDataContractSerializerFormatters();
            /*
             * AddXmlDataContractSerializerFormatters allow response in XML if requested in header.
             * setupAction.ReturnHttpNotAcceptable allows to throw error if use request the response in unknown format like other than JSON AND XML which system doesnt know.
             */
            services.AddSingleton<IRestaurantData, InMemoryRestaurantData>();

            /* Swagger Configuration */
            services.AddSwaggerGen(setupAction =>
            {
                setupAction.SwaggerDoc(
                    "RestaurantAPIDocumentation"
                    , new Microsoft.OpenApi.Models.OpenApiInfo()
                    {
                        Title = "Restaurant API",
                        Version = "1"
                    });

                //This is to show description in swagger page, but for this add comments on each method by type 3 time /
                //Update the project properties, build --> XML Documentation file, remove all keep only name.
                // It will generate description wherever we put comments either properties or method.
                var xmlFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlFileFullPath = Path.Combine(AppContext.BaseDirectory, xmlFileName);
                setupAction.IncludeXmlComments(xmlFileFullPath);
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //This is how we can customize the exception in prod
                app.UseExceptionHandler(appBuilder =>
                {
                    appBuilder.Run(async context =>
                    {
                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync("An unexpected fault happened. Try again later");
                    });
                });
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(setupAction =>
            {
                setupAction.SwaggerEndpoint(
                    "/swagger/RestaurantAPIDocumentation/swagger.json",
                    "Restaurant API Swagger Page"
                    );

                setupAction.RoutePrefix = "";  // Currently swagger is available at - http://localhost:5000/swagger/index.html, we want it on root.

            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
