using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Models;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RazorPages
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
            services.AddRazorPages(x =>
            {
                x.Conventions.AuthorizeFolder("/Users").AllowAnonymousToPage("/Users/Index");
            });
            services.AddSingleton<IService<User>>(x => new Service<User>(new List<User> {
                new User { Id= 1, Username = "Username1", Password = "Password1", Role = Roles.Read | Roles.Create },
                new User { Id= 2, Username = "Username2", Password = "Password2", Role = Roles.Read | Roles.Update },
                new User { Id= 3, Username = "Username3", Password = "Password3", Role = Roles.Read | Roles.Create | Roles.Update | Roles.Delete }
                }
            ));


            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(x =>
                {
                    x.LoginPath = "/Login";
                    x.LogoutPath = "/Login/Logout";
                    x.AccessDeniedPath = "/";
                    x.ExpireTimeSpan = TimeSpan.FromMinutes(5);
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
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
