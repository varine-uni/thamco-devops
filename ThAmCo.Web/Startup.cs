using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using ThAmCo.Web.Data;
using ThAmCo.Web.Services;

namespace ThAmCo.Web
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
            services.AddControllersWithViews();

            services.AddDbContext<InventoryDbContext>(options
                => options.UseSqlite(Configuration.GetConnectionString("InventoryDbConnection")));

            services.AddHttpClient<IInventoryService, InventoryService>();

            services.AddAuth0WebAppAuthentication(options => {
                options.Domain = Configuration["Auth0:Domain"]; ;
                options.ClientId = Configuration["Auth0:ClientId"]; ;
            });

            services.AddSingleton<Auth0ManagementApiClient>(provider =>
            {
                var auth0Domain = Configuration["Auth0:Domain"];
                var clientId = Configuration["Auth0:ClientId"];
                var clientSecret = Configuration["Auth0:ClientSecret"];

                return new Auth0ManagementApiClient(auth0Domain, clientId, clientSecret);
            });

            services.AddScoped<EmployeeService>();

            services.AddScoped<ManagerService>();

            services.AddScoped<StaffService>();
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

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
