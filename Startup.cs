using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Turnos.Models;

namespace Turnos
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
            
            //CONFIGURACION PARA LA BBDD
            //usar "using" para referenciar MODELS, ESTE METODO HACE QUE SE HAGA LA "CONEXIÓN A SQL"
            services.AddDbContext<TurnosContext>(opciones => opciones.UseSqlServer(Configuration.GetConnectionString("TurnosContext")));    //Se agrega para establecer la conexion con la BBDD SQL, se especifica la clase Context de Models
            //tipo de contexto se especifica la clase Context y tambien se especifica el motor de BBDD en este caso SQL
            //el objeto opciones estara conectandose con SQL
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
            //app.UseHttpsRedirection(); // ->https://localhost:5001
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");//aqui se define el controlador principal, cuando abra tu pagina se abrira con index
            });
        }
    }
}
