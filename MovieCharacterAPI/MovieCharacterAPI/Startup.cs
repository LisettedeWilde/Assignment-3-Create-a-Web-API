using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MovieCharacterAPI.Models.Data;
using System;

namespace MovieCharacterAPI
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

            services.AddControllers();
            services.AddDbContext<MovieCharacterDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
            );
            services.AddAutoMapper(typeof(Startup)); //TODO: figure out function of this
            services.AddSwaggerGen(c => // TODO: edit this Swagger documentation
            {
                c.SwaggerDoc("v1", new OpenApiInfo { 
                    Title = "MovieCharacterAPI", 
                    Version = "v1",
                Description = "An ASP.Net Core Web API for a MovieDb",
                Contact = new OpenApiContact()
                {
                    Name = "Lisette de Wilde",
                    Email = string.Empty,
                    Url = new Uri("https://linkedin.com/"),
                },
                License = new OpenApiLicense()
                {
                    Name = "Use under MIT",
                    Url = new Uri("https://opensource.org/licenses/MIT"),
                }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MovieCharacterAPI v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
