
using TrainingTask.ApplicationCore.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TrainingTask.ApplicationCore.Dto;
using TrainingTask.Common;
using TrainingTask.Infrastructure.Models;
using TrainingTask.Web.Converters;
using Common.Logger;
using TrainingTask.Web.ViewModels;
using TrainingTask.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;

namespace TrainingTask.Web
{
    public class Startup
    {
        private readonly string connectionString;
        private readonly string LogsPath;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            connectionString = Configuration["ConnectionStrings:TrainingTaskDB"];
            LogsPath = Configuration["LoggerPaths:FileLogger"];
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddSingleton<ILogger>(x => new FileLogger(LogsPath));

            services.AddSingleton<IConvert<EmployeeVm, EmployeeDto>, Web.Converters.EmployeeDtoConverter>();
            services.AddSingleton<IConvert<EmployeeDto, EmployeeVm>, EmployeeVmConverter>();
            services.AddSingleton<IConvert<ProjectVm, ProjectDto>, Web.Converters.ProjectDtoConverter>();
            services.AddSingleton<IConvert<ProjectDto, ProjectVm>, ProjectVmConverter>();
            services.AddSingleton<IConvert<ProjectTaskVm, ProjectTaskDto>, Web.Converters.ProjectTaskDtoConverter>();
            services.AddSingleton<IConvert<ProjectTaskDto, ProjectTaskVm>, ProjectTaskVmConverter>();

            services.AddSingleton<IConvert<Employee, EmployeeDto>, ApplicationCore.Converters.EmployeeDtoConverter>();
            services.AddSingleton<IConvert<EmployeeDto, Employee>, ApplicationCore.Converters.EmployeeConverter>();
            services.AddSingleton<IConvert<Project, ProjectDto>, ApplicationCore.Converters.ProjectDtoConverter>();
            services.AddSingleton<IConvert<ProjectDto, Project>, ApplicationCore.Converters.ProjectConverter>();
            services.AddSingleton<IConvert<ProjectTask, ProjectTaskDto>, ApplicationCore.Converters.ProjectTaskDtoConverter>();
            services.AddSingleton<IConvert<ProjectTaskDto, ProjectTask>, ApplicationCore.Converters.ProjectTaskConverter>();

            services.AddScoped<IService<EmployeeDto>, EmployeeService>();
            services.AddScoped<IService<ProjectDto>, ProjectService>();
            services.AddScoped<IProjectTaskService<ProjectTaskDto>, ProjectTaskService>();

            services.AddScoped<IRepository<Employee>>(serviceProvider => new EmployeeRepository(connectionString, serviceProvider.GetService<ILogger>()));
            services.AddScoped<IRepository<Project>>(serviceProvider => new ProjectRepository(connectionString, serviceProvider.GetService<ILogger>()));
            services.AddScoped<IProjectTaskRepository<ProjectTask>>(serviceProvider => new ProjectTaskRepository(connectionString, serviceProvider.GetService<ILogger>()));
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
