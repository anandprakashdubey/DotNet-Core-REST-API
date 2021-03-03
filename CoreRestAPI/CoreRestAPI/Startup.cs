using DBLayer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
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


            services.AddControllers(setupAction => {
                setupAction.ReturnHttpNotAcceptable = true;
            }).AddXmlDataContractSerializerFormatters();
            /*
             * AddXmlDataContractSerializerFormatters allow response in XML if requested in header.
             * setupAction.ReturnHttpNotAcceptable allows to throw error if use request the response in unknown format like other than JSON AND XML which system doesnt know.
             */
            services.AddSingleton<IRestaurantData, InMemoryRestaurantData>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
