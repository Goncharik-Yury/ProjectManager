using ProjectManager.ApplicationCore.Converters;
using System;
using System.Collections.Generic;
using ProjectManager.ApplicationCore.Dto;
using ProjectManager.Common;
using ProjectManager.Infrastructure.Models;
using ProjectManager.Infrastructure.Repositories;

namespace ProjectManager.ApplicationCore.Repository
{
    public class ProjectTaskService : IProjectTaskService<ProjectTaskDto>
    {
        private readonly IRepository<ProjectTask> ProjectTaskRepository;
        private readonly IConvert<ProjectTask, ProjectTaskDto> ProjectTaskDtoConverter;
        private readonly IConvert<ProjectTaskDto, ProjectTask> ProjectTaskConverter;

        public ProjectTaskService(IRepository<ProjectTask> projectTaskRepository, IConvert<ProjectTask, ProjectTaskDto> projectTaskDtoConverter, IConvert<ProjectTaskDto, ProjectTask> projectTaskConverter)
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
            ProjectTask ProjectTask = ProjectTaskRepository.GetSingle(id);
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
            IList<ProjectTask> ProjectTask = (ProjectTaskRepository as IRepositoryExtention<ProjectTask>).GetAllByProjectId(id);
            return ProjectTaskDtoConverter.Convert(ProjectTask);
        }
    }
}
