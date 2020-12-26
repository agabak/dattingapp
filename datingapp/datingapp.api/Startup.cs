using datingapp.api.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "datingapp.api", Version = "v1" });
            });

            services.AddDbContext<DataContext>(opts => 
            {
                opts.UseSqlServer(_config.GetConnectionString("DefaultConnection"));
            });

            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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