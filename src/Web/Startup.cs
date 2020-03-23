using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using ApplicationCore.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TrainingTask.ApplicationCore.Dto;
using TrainingTask.Web.Converters;
using TrainingTask.Web.Logger;
using TrainingTask.Web.ViewModels;

namespace TrainingTask.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            string ConnectionString = Configuration["ConnectionStrings:TrainingTaskDB"]; // TODO: use this connection string. And move it to the right place.
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddSingleton<ILogger, FileLogger>(); // TODO: mabe change lifetime
            services.AddSingleton<IConvertWeb<EmployeeVm, EmployeeDto>, EmployeeWebConverter>();
            services.AddSingleton<IConvertWeb<ProjectVm, ProjectDto>, ProjectWebConverter>();
            services.AddSingleton<IConvertWeb<ProjectTaskVm, ProjectTaskDto>, ProjectTaskWebConverter>();
            //services.AddSingleton<IRepositoryService<ProjectTaskDto>, ProjectTaskRepositoryService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Employee}/{action=Index}/{id?}");
            });


        }
    }
}
