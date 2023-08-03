using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Session;
using System.Configuration;

namespace AdministracionContractual
{
    public class Startup
    {
        //configuración para el appsettings.json
        public IConfiguration _Config { get; }
        public Startup(IConfiguration configuration) {
            _Config = configuration;
        }
        //configuración para el appsettings.json

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //En esta línea se configura las variables de Sesión para que tengan vida en toda la solución de "ApegoContractual"  
            services.AddSession(opts => {
                opts.Cookie.IsEssential = true;
                opts.IdleTimeout = TimeSpan.FromMinutes(40);
            });
            services.AddDistributedMemoryCache();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>(); //La línea sirve para mandar a llamar las sesiones en las vistas 
            //En esta línea se configura las variables de Sesión para que tengan vida en toda la solución de "ApegoContractual"
            
            //Esta línea lee la sección del appsetting.json específicamente la sección del "EndPoint" 
            services.Configure<EndPointAdmon>(_Config.GetSection("EndPointAdmon"));
            //Esta línea lee la sección del appsetting.json específicamente la sección del "EndPoint"
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseCookiePolicy();
            app.UseSession();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            //Definición de la pagina de inicio en el sistema [Controlador],[Acción]   
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Login}/{action=Index}/{id?}");
                
            });
            //Definición de la pagina de inicio en el sistema [Controlador],[Acción]   
        }
    }
}
