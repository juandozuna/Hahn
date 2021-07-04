using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Hahn.ApplicationProcess.February2021.Web
{
    using Data;
    using Data.Repositories;
    using Domain.Managers;
    using Domain.Repositories;
    using Microsoft.EntityFrameworkCore;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient();

            services.AddCors(opts =>
            {
                opts.AddDefaultPolicy(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            });

            ConfigureDependencies(services);

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new OpenApiInfo {Title = "Hahn.ApplicationProcess.February2021.Web", Version = "v1"});
            });

            services.AddDbContext<HahnDbContext>(
                // opts => opts.UseSqlServer(Configuration.GetConnectionString("MainConnection"))
                opts => opts.UseInMemoryDatabase("Sample")
            );
        }


        private void ConfigureDependencies(IServiceCollection services)
        {
            services.AddScoped<IAssetRepository, AssetRepository>();
            services.AddScoped<ICountryRepository, CountryRepository>();

            services.AddScoped<AssetManager, AssetManager>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Hahn.ApplicationProcess.February2021.Web v1"));
            }

            app.UseCors();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}