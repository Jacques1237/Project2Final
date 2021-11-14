using System.IO;
using Gallery.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;

namespace Gallery
{
    public class Startup
    {
        private object services;
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<UploadImageDBContext>(options =>
            options.UseSqlServer(@"Data Source=.;Initial Catalog=UpdateImagesDb;Integrated Security=True;MultipleActiveResultSets=true"));

            services.AddControllersWithViews();
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            services.Configure<FormOptions>(o => {
                o.ValueLengthLimit = int.MaxValue;
                o.MultipartBodyLengthLimit = int.MaxValue;
                o.MemoryBufferThreshold = int.MaxValue;
            });

            services.AddCors(options => options.AddPolicy("CorsPolicy",
             builder =>
             {
                 builder.AllowAnyMethod()
                 .AllowAnyHeader()
                 .AllowAnyOrigin();
             }));

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("CorsPolicy");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();
            //app.UseCors(MyAllowSpecificOrigins);
           // app.UseCors("CorsPolicy");
            app.UseStaticFiles();
            
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Images")),
                RequestPath = new PathString("/Images")
            });

            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

           // app.UseHttpsRedirection();
            //app.UseStaticFiles();
            //if (!env.IsDevelopment())
            //{
               // app.UseSpaStaticFiles();
            //}

           // app.UseRouting();

           // app.UseEndpoints(endpoints =>
           // {
              //  endpoints.MapControllerRoute(
                   // name: "default",
                   // pattern: "{controller}/{action=Index}/{id?}");
           // });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                

                if (env.IsDevelopment())
                {
                    spa.UseProxyToSpaDevelopmentServer("http://localhost:4200");
                   // spa.UseAngularCliServer(npmScript: "start");
                }
                else
                {
                    spa.Options.SourcePath = "ClientApp";
                }
            });

            
        }
    }
}
