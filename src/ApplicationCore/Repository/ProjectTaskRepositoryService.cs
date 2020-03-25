using TrainingTask.ApplicationCore.Converters;
using System;
using System.Collections.Generic;
using TrainingTask.ApplicationCore.Dto;
using TrainingTask.Common;
using TrainingTask.Infrastructure.Models;
using TrainingTask.Infrastructure.Repositories;

namespace TrainingTask.ApplicationCore.Repository
{
    public class ProjectTaskRepositoryService : IProjectTaskRepositoryService<ProjectTaskDto>
    {
        IProjectTaskRepository<ProjectTask> ProjectTaskRepository;
        IRepositoryService<ProjectDto> ProjectRepositoryService;
        IRepositoryService<EmployeeDto> EmployeeRepositoryService;

        IConvert<ProjectTask, ProjectTaskDto> ProjectTaskDtoConverter;
        IConvert<ProjectTaskDto, ProjectTask> ProjectTaskConverter;

        public ProjectTaskRepositoryService(IRepositoryService<ProjectDto> projectRepositoryService, 
            IRepositoryService<EmployeeDto> employeeRepositoryService,
            IProjectTaskRepository<ProjectTask> projectTaskRepository,
            IConvert<ProjectTask, ProjectTaskDto> projectTaskDtoConverter,
            IConvert<ProjectTaskDto, ProjectTask> projectTaskConverter
            )
        {
            ProjectTaskRepository = projectTaskRepository;
            ProjectRepositoryService = projectRepositoryService;
            EmployeeRepositoryService = employeeRepositoryService;

            ProjectTaskDtoConverter = projectTaskDtoConverter;
            ProjectTaskConverter = projectTaskConverter;
        }

        public void Create(ProjectTaskDto item)
        {
            ProjectTask ProjectTask = ProjectTaskConverter.Convert(item);
            ProjectTaskRepository.Create(ProjectTask);
        }

        public void Delete(int id)
        {
            ProjectTaskRepository.Delete(id);
        }

        public IList<ProjectTaskDto> GetAll()
        {
            IList<ProjectTask> ProjectTasks = ProjectTaskRepository.GetAll();
            List<ProjectTaskDto> ProjectTasksDto = new List<ProjectTaskDto>();
            foreach (var item in ProjectTasks)
            {
                ProjectTasksDto.Add(ProjectTaskDtoConverter.Convert(item));
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
            ProjectTask ProjectTask = ProjectTaskRepository.GetSingle(id);
            ProjectTaskDto ProjectTaskDto = ProjectTaskDtoConverter.Convert(ProjectTask);
            ProjectTaskDto.ProjectShortName = GetProjectShortName(ProjectTaskDto.ProjectId);
            ProjectTaskDto.EmployeeFullName = GetEmployeeFullName(ProjectTaskDto.EmployeeId);

            return ProjectTaskDto;
        }

        public void Update(ProjectTaskDto item)
        {
            ProjectTask ProjectTask = ProjectTaskConverter.Convert(item);
            ProjectTaskRepository.Update(ProjectTask);
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

        public IList<ProjectTaskDto> GetAllByProjectId(int id)
        {
            IList<ProjectTask> ProjectTask = ProjectTaskRepository.GetAllByProjectId(id);
            return ProjectTaskDtoConverter.ConvertAll(ProjectTask);
        }
    }
}
