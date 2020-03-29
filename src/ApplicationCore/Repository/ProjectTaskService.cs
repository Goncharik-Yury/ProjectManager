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
        private readonly IConvert<ProjectTask, ProjectTaskDto> ProjectTaskDtoConverter;
        private readonly IConvert<ProjectTaskDto, ProjectTask> ProjectTaskConverter;

        public ProjectTaskService(IProjectTaskRepository<ProjectTask> projectTaskRepository, IConvert<ProjectTask, ProjectTaskDto> projectTaskDtoConverter, IConvert<ProjectTaskDto, ProjectTask> projectTaskConverter)
        {
            ProjectTaskRepository = projectTaskRepository;
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
            IList<ProjectTaskDto> ProjectTasksDto = ProjectTaskDtoConverter.Convert(ProjectTasks);
            return ProjectTasksDto;
        }

        public ProjectTaskDto Get(int id)
        {
            ProjectTask ProjectTask = ProjectTaskRepository.Get(id);
            ProjectTaskDto ProjectTaskDto = ProjectTaskDtoConverter.Convert(ProjectTask);
            return ProjectTaskDto;
        }

        public void Update(ProjectTaskDto item)
        {
            ProjectTask ProjectTask = ProjectTaskConverter.Convert(item);
            ProjectTaskRepository.Update(ProjectTask);
        }

        public IList<ProjectTaskDto> GetAllByProjectId(int id)
        {
            IList<ProjectTask> ProjectTask = ProjectTaskRepository.GetAllByProjectId(id);
            return ProjectTaskDtoConverter.Convert(ProjectTask);
        }
    }
}
