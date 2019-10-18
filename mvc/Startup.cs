using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;
using Microsoft.Extensions.Logging;
using testing1.Models;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace testing1
{
    public class Startup
    {
        private IConfiguration _config;
        public Startup(IConfiguration config){
            _config = config;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<AppDbContext>(options => options.UseMySql(_config.GetConnectionString("TestingConnection"))); //CONFIG DI appsettings.Development.json

            services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

            services.AddMvc(option => option.EnableEndpointRouting = false);

            //    DEPEDENCY INJECTION

            //AddSingleton ==> hanya mengcreate 1 instance saja di tiap modelnya pada semua HTTP REQUEST
            //AddScoped ==> tidak membuat instance  apabila melakukan request yang sama, tpi kalo HTTP request berbeda membuat instance baru
            //AddTransient ==> membuat instance baru di semua request

            // IN-MEMORY OBJECT :
                //  services.AddSingleton<IEmployeeRepository,MockEmployeeRepository>();
                // services.AddScoped<IEmployeeRepository, MockEmployeeRepository>();
                // services.AddTransient<IEmployeeRepository, MockEmployeeRepository>();

            // DATABASE SQL SERVER:
                services.AddScoped<IEmployeeRepository, SQLEmployeeRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                DeveloperExceptionPageOptions developerExceptionPageOptions = new DeveloperExceptionPageOptions()
                {
                    SourceCodeLineCount = 20
                };
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            //app.Use(async (context,next)=>{
            //    logger.LogInformation("testing doang");
            //await next();
            //});

            //untuk menggunakan static file;
            //menggunakan default file;
            //DefaultFilesOptions defaultFilesOptions = new DefaultFilesOptions();
            //defaultFilesOptions.DefaultFileNames.Clear();
            //defaultFilesOptions.DefaultFileNames.Add("foo.html");
            //app.UseDefaultFiles(defaultFilesOptions);
            app.UseStaticFiles();

            //MVC
            // app.UseMvcWithDefaultRoute();
            // app.UseMvc(routes=>{
            //     routes.MapRoute("default","{controller=Home}/{action=Index}/{id?}");
            // });
            app.UseMvc();
            app.UseEndpoints(endpoints =>
            {
               
                endpoints.MapGet("/d", async context =>
                {
                    throw new Exception("error");
                    logger.LogInformation("f");
                    await context.Response.WriteAsync(_config["testing"]);
                });
                endpoints.MapGet("/ad", async context =>
             {
                 Debug.WriteLine("ada disini : " + endpoints);
                 await context.Response.WriteAsync("asd");
             });
            });
        }
    }
}