using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AllTech.DataLayer.Context;
using AllTech.Services.Repositories;
using AllTech.Services.Services;
using AllTech.Services.Services.Interfaces;
using AllTech.Utilities.Convertor;
using AllTech.Web.External_Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

namespace AllTech.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            #region External Identity

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AllTechDbContext>()
                .AddDefaultTokenProviders();

            #endregion


            #region Authentication

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie(options =>
            {
                options.LoginPath = "/Login";
                options.LogoutPath = "/Logout";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
            }).AddGoogle(options =>
            {
                IConfigurationSection googleAuthNSection =
                Configuration.GetSection("Authentication:Google");

                options.ClientId = "265376837462-2uaqjbms8v2t1ujnei1t2hct7q1oqegq.apps.googleusercontent.com";
                options.ClientSecret = "KSSyMhAVI99iRm1krup9Nrub";
            });

            #endregion

            #region UploadImage

            services.AddSingleton<IFileProvider>(
            new PhysicalFileProvider(
                Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));

            #endregion

            #region AddDbContext

            services.AddDbContext<AllTechDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("AllTechDbContext")));

            #endregion

            #region IoC

            services.AddTransient<INewsGroupRepository, NewsGroupRepository>();
            services.AddTransient<INewsRepository, NewsRepository>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IPermissionService,PermissionService>();
            services.AddTransient<IViewRenderService, RenderViewToString>();

            #endregion


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvcWithDefaultRoute();
            app.UseMvc(routes =>
            {
                
                routes.MapRoute(
                  name: "areas",
                  template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                
                routes.MapRoute("Default", "{controller=Home}/{action=Index}/{id?}");
            });

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }
    }
}
