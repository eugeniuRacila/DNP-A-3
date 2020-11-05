using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ResourcesAPI.Services;
using System;

namespace ResourcesAPI
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
            services.AddSingleton(new PersistenceService(FamilyService._familiesPath));
            services.AddSingleton<FamilyService>();
            services.AddSingleton<AdultService>();
            //services.AddSingleton<ChildService>();
            services.AddSingleton<PetService>();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Something similar to morgan
            app.Use(async (context, next) =>
            {
                DateTime start = DateTime.Now;

                await next.Invoke();

                DateTime end = DateTime.Now;

                Console.BackgroundColor = ConsoleColor.DarkGreen;
                Console.Write($" {context.Request.Method} ");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write($" {context.Request.Path.Value}");
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.Write($" {context.Response.StatusCode}");
                Console.ResetColor();

                Console.WriteLine($" {(end - start).TotalMilliseconds:N3}ms");
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
