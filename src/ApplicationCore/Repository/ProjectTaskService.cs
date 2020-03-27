using TrainingTask.ApplicationCore.Converters;
using System;
using System.Collections.Generic;
using TrainingTask.ApplicationCore.Dto;
using TrainingTask.Common;
using TrainingTask.Infrastructure.Models;
using TrainingTask.Infrastructure.Repositories;

namespace TrainingTask.ApplicationCore.Repository
{
    public class ProjectTaskService : IProjectTaskService<ProjectTaskDto>
    {
        private readonly IProjectTaskRepository<ProjectTask> ProjectTaskRepository;
        private readonly IService<ProjectDto> ProjectRepositoryService;
        private readonly IService<EmployeeDto> EmployeeRepositoryService;

        private readonly IConvert<ProjectTask, ProjectTaskDto> ProjectTaskDtoConverter;
        private readonly IConvert<ProjectTaskDto, ProjectTask> ProjectTaskConverter;

        public ProjectTaskService(IService<ProjectDto> projectRepositoryService, 
            IService<EmployeeDto> employeeRepositoryService,
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

        public ProjectTaskDto Get(int id)
        {
            ProjectTask ProjectTask = ProjectTaskRepository.Get(id);
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
            return ProjectRepositoryService.Get(id).ShortName;
        }

        private string GetEmployeeFullName(int? id)
        {
            if (id == null) return "";
            EmployeeDto EmployeeDto = EmployeeRepositoryService.Get((int)id);
            string EmployeeFullName = $"{EmployeeDto.LastName} {EmployeeDto.FirstName} {EmployeeDto.Patronymic}";

            return EmployeeFullName;
        }

        public IList<ProjectTaskDto> GetAllByProjectId(int id)
        {
            IList<ProjectTask> ProjectTask = ProjectTaskRepository.GetAllByProjectId(id);
            return ProjectTaskDtoConverter.Convert(ProjectTask);
        }
    }
}
