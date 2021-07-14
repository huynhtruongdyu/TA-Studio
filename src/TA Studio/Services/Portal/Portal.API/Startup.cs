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
using Portal.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Portal.API
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
            //Enable CORS
            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", option => option.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });

            //Add DbContext
            services.AddDbContext<PortalContext>(options => options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IPortalContext>(provider => provider.GetService<PortalContext>());
            services.AddScoped<DbContext, PortalContext>();
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));

            #region Configure for Repositories
            var allRepositoryInterfaces = Assembly.GetAssembly(typeof(IRepository<>))
                ?.GetTypes().Where(t => t.Name.EndsWith("Repository")).ToList();
            var allRepositoryImplements = Assembly.GetAssembly(typeof(EfRepository<>))
                ?.GetTypes().Where(t => t.Name.EndsWith("Repository")).ToList();

            foreach (var repositoryType in allRepositoryInterfaces.Where(t => t.IsInterface))
            {
                var implement = allRepositoryImplements.FirstOrDefault(c => c.IsClass && repositoryType.Name.Substring(1) == c.Name);
                if (implement != null) services.AddScoped(repositoryType, implement);
            }

            #endregion Configure for Repositories

            #region Configure for Serivces
            //var allServicesInterfaces = Assembly.GetAssembly(typeof(IService))
            //    ?.GetTypes().Where(t => t.Name.EndsWith("Service")).ToList();
            //var allServiceImplements = Assembly.GetAssembly(typeof(Service))
            //    ?.GetTypes().Where(t => t.Name.EndsWith("Service")).ToList();

            //foreach (var serviceType in allServicesInterfaces.Where(t => t.IsInterface))
            //{
            //    var implement = allServiceImplements.FirstOrDefault(c => c.IsClass && serviceType.Name.Substring(1) == c.Name);
            //    if (implement != null) services.AddScoped(serviceType, implement);
            //}

            //services.AddScoped<IWorkContext, WorkContext>();

            #endregion Configure for Serivces

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Portal.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //Enable CORS
            app.UseCors(option => option.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Portal.API v1"));
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
