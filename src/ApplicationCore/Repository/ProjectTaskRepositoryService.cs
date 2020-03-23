using ApplicationCore.Converters;
using System;
using System.Collections.Generic;
using TrainingTask.ApplicationCore.Dto;
using TrainingTask.Infrastructure.Models;
using TrainingTask.Infrastructure.Repositories;

namespace ApplicationCore.Repository
{
    public class ProjectTaskRepositoryService : IProjectTaskRepositoryService<ProjectTaskDto>
    {
        IProjectTaskRepository<ProjectTask> Repository;
        IConvertBll<ProjectTask, ProjectTaskDto> Converter;

        ProjectRepositoryService ProjectRepositoryService;
        EmployeeRepositoryService EmployeeRepositoryService;

        public ProjectTaskRepositoryService()
        {
            Repository = new ProjectTaskRepository();
            Converter = new ProjectTaskBloConverter();

            ProjectRepositoryService = new ProjectRepositoryService();
            EmployeeRepositoryService = new EmployeeRepositoryService();
        }

        public void Create(ProjectTaskDto item)
        {
            ProjectTask ProjectTask = Converter.Convert(item);
            Repository.Create(ProjectTask);
        }

        public void Delete(int id)
        {
            Repository.Delete(id);
        }

        public List<ProjectTaskDto> GetAll()
        {
            List<ProjectTask> ProjectTasks = Repository.GetAll();
            List<ProjectTaskDto> ProjectTasksDto = new List<ProjectTaskDto>();
            foreach (var item in ProjectTasks)
            {
                ProjectTasksDto.Add(Converter.Convert(item));
            }

            foreach (var item in ProjectTasksDto)
            {
                item.ProjectShortName = GetProjectShortName(item.ProjectId);
                item.EmployeeFullName = GetEmployeeFullName(item.EmployeeId);
            }

            return ProjectTasksDto;
        }

        public ProjectTaskDto GetSingle(int id)
        {
            ProjectTask ProjectTask = Repository.GetSingle(id);
            ProjectTaskDto ProjectTaskDto = Converter.Convert(ProjectTask);
            ProjectTaskDto.ProjectShortName = GetProjectShortName(ProjectTaskDto.ProjectId);
            ProjectTaskDto.EmployeeFullName = GetEmployeeFullName(ProjectTaskDto.EmployeeId);

            return ProjectTaskDto;
        }

        public void Update(ProjectTaskDto item)
        {
            ProjectTask ProjectTask = Converter.Convert(item);
            Repository.Update(ProjectTask);
        }

        private string GetProjectShortName(int id)
        {
            return ProjectRepositoryService.GetSingle(id).ShortName;
        }

        private string GetEmployeeFullName(int? id)
        {
            if (id == null) return "";
            EmployeeDto EmployeeDto = EmployeeRepositoryService.GetSingle((int)id);
            string EmployeeFullName = $"{EmployeeDto.LastName} {EmployeeDto.FirstName} {EmployeeDto.Patronymic}";

            return EmployeeFullName;
        }

        public List<ProjectTaskDto> GetAllByProjectId(int id)
        {
            List<ProjectTask> ProjectTask = Repository.GetAllByProjectId(id);
            return Converter.ConvertList(ProjectTask);
        }
    }
}
