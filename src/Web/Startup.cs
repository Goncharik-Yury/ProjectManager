
using ProjectManager.ApplicationCore.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ProjectManager.ApplicationCore.Dto;
using ProjectManager.Common;
using ProjectManager.Infrastructure.Models;
using ProjectManager.Web.Converters;
using Common.Logger;
using ProjectManager.Web.ViewModels;
using ProjectManager.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data.SqlClient;
using ProjectManager.Infrastructure.Converters;

namespace ProjectManager.Web
{
    public class Startup
    {
        private readonly string connectionString;
        private readonly string LogsPath;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            connectionString = Configuration["ConnectionStrings:ProjectManagerDB"];
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

            services.AddScoped<IConvertDb<SqlDataReader, Employee>, EmployeeConverterDb>();
            services.AddScoped<IConvertDb<SqlDataReader, Project>, ProjectConverterDb>();
            services.AddScoped<IConvertDb<SqlDataReader, ProjectTask>, ProjectTaskConverterDb>();

            services.AddScoped<IRepository<Employee>>(serviceProvider => new EmployeeRepository(connectionString, serviceProvider.GetService<IConvertDb<SqlDataReader, Employee>>(), serviceProvider.GetService<ILogger>()));
            services.AddScoped<IRepository<Project>>(serviceProvider => new ProjectRepository(connectionString, serviceProvider.GetService<IConvertDb<SqlDataReader, Project>>(), serviceProvider.GetService<ILogger>()));
            services.AddScoped<IProjectTaskRepository<ProjectTask>>(serviceProvider => new ProjectTaskRepository(connectionString, serviceProvider.GetService<IConvertDb<SqlDataReader, ProjectTask>>(), serviceProvider.GetService<ILogger>()));
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
                    pattern: "{controller=Project}/{action=index}/{id?}");
                //pattern: "{controller=Project}/{action=Edit}/{id=2}");

            });


        }
    }
}
