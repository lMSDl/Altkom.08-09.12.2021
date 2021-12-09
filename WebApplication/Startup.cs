using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Models;
using Models.Validators;
using Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication
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
            services.AddControllersWithViews().AddViewLocalization()
                .AddDataAnnotationsLocalization(x => x.DataAnnotationLocalizerProvider = (type, facotry) => facotry.Create(typeof(Program)))
                .AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<UserValidator>()) ;

            //services.AddTransient<IValidator<User>, UserValidator>();


            services.AddSingleton<IService<User>>(x => new Service<User>( new List<User> {
                new User { Id= 1, Username = "Username1", Password = "Password1", Role = Roles.Read | Roles.Create },
                new User { Id= 2, Username = "Username2", Password = "Password2", Role = Roles.Read | Roles.Update },
                new User { Id= 3, Username = "Username3", Password = "Password3", Role = Roles.Read | Roles.Create | Roles.Update | Roles.Delete }
                }
            ));


            services.AddLocalization(x => x.ResourcesPath = "Resources");
            services.Configure<RequestLocalizationOptions>(x =>
           {
               x.SetDefaultCulture("en-us");
               x.AddSupportedCultures("en-us", "pl");
               x.AddSupportedUICultures("en-us", "pl");
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRequestLocalization();

            app.UseRouting();

            app.UseAuthorization();


            app.UseStaticFiles(new StaticFileOptions
            {
                //using Microsoft.Extensions.FileProviders;
                FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, "Downloads")),
                RequestPath = "/pliki",
                //using Microsoft.AspNetCore.Http;
                OnPrepareResponse = x => x.Context.Response.Headers.Append("Cache-Control", "public, max-age=60000")
            });
            app.UseDirectoryBrowser(new DirectoryBrowserOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, "Downloads")),
                RequestPath = "/pliki"
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "Hello",
                    pattern: "Greetings/{controller=Hello}/{action=StartPage}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

            });
        }
    }
}
