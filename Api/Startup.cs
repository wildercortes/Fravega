using Api.Middlewares;
using Data;
using Data.Interfaces;
using Data.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;

namespace Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
       // private const string corsPolicyName = "CorsPolicy";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            VersioningConfiguration(services);

            services.AddDbContext<DataContext>(options => options
               .UseSqlServer(Configuration.GetConnectionString("Sql"),
               opt => opt.CommandTimeout((int)TimeSpan.FromMinutes(5).TotalSeconds)));

            //services.AddCors(options =>
            //{
            //    options.AddPolicy(corsPolicyName,
            //        builder => builder
            //            .AllowAnyOrigin()
            //            .AllowAnyMethod()
            //            .AllowAnyHeader());
            //});

            services.AddScoped<IBranchOfficeRepository, BranchOfficeRepository>();
            services.AddControllers();

            AddSwaggerConfiguration(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Fravega");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseMiddleware(typeof(ExceptionHandlingMiddleware));

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<DataContext>();
                context.Database.Migrate();
            }
        }


        #region Versioning Configuration

        private static void VersioningConfiguration(IServiceCollection services)
        {
            services.AddApiVersioning(options => options.ReportApiVersions = true);
            services.AddVersionedApiExplorer(
                options =>
                {
                    options.GroupNameFormat = "'v'VVV";
                    options.SubstituteApiVersionInUrl = true;
                    options.AssumeDefaultVersionWhenUnspecified = true;
                    options.DefaultApiVersion = new ApiVersion(1, 0);
                });
        }

        #endregion

        #region Swagger Configuration

        private static void AddSwaggerConfiguration(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "API",
                    Description = "QPIN API with ASP.NET Core 3.0",
                    Contact = new OpenApiContact()
                    {
                        Name = "Tafsir Dadeh Zarrin",
                        Url = new Uri("http://www.tdz.co.ir")
                    }
                });
            });
        }

        #endregion
    }
}
