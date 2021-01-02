using datingapp.api.Data;
using datingapp.api.Extensions;
using datingapp.api.Interfaces;
using datingapp.api.Middleware;
using datingapp.api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace datingapp.api
{
    public class Startup
    {
        private readonly IConfiguration _config;
        public Startup(IConfiguration configuration)
        {
            _config = configuration;
        }

        // This method gets cal_cled by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {


            services.AddApplicationServices(_config);
            services.AddIdentityServices(_config);

            services.AddControllers();
            services.AddCors();
        
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ExceptionMiddleware>();

            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "datingapp.api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors(policy => 
            {
                policy.AllowAnyHeader().AllowAnyMethod()
                      .WithOrigins("https://localhost:4200",
                                    "http://localhost:4200");
            });

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

// install microsoft.entityframeworkcore.sqlserver 5.0.01 version
// net 5 template come with swagger set up already - superb