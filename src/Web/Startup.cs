
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
using TrainingTask.Web.Logger;
using TrainingTask.Web.ViewModels;
using TrainingTask.Infrastructure.Repositories;
using TrainingTask.Infrastructure.SqlDataReaders;
using Microsoft.AspNetCore.Mvc;
using System;

namespace TrainingTask.Web
{
    public class Startup
    {
        private string ConnectionString;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            ConnectionString = Configuration["ConnectionStrings:TrainingTaskDB"];
            Console.WriteLine("!!!!!!!!!!!!!!!!!!!" + ConnectionString);
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSingleton<ILogger, FileLogger>();

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

            services.AddSingleton<IRepositoryService<EmployeeDto>, EmployeeRepositoryService>();
            services.AddSingleton<IRepositoryService<ProjectDto>, ProjectRepositoryService>();
            services.AddSingleton<IProjectTaskRepositoryService<ProjectTaskDto>, ProjectTaskRepositoryService>();

            services.AddSingleton<IRepository<Employee>, EmployeeRepository>();
            services.AddSingleton<IRepository<Project>, ProjectRepository>();
            services.AddSingleton<IProjectTaskRepository<ProjectTask>, ProjectTaskRepository>();

            services.AddSingleton<ISqlDataReader<Employee>>(x => new EmployeeSqlDataReader(ConnectionString));
            services.AddSingleton<ISqlDataReader<Project>>(x => new ProjectSqlDataReader(ConnectionString));
            services.AddSingleton<ISqlDataReader<ProjectTask>>(x => new ProjectTaskSqlDataReader(ConnectionString));
            //services.AddSingleton<ISqlDataReader<Employee>, EmployeeSqlDataReader>();
            //services.AddSingleton<ISqlDataReader<Project>, ProjectSqlDataReader>();
            //services.AddSingleton<ISqlDataReader<ProjectTask>, ProjectTaskSqlDataReader>();
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
