using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using TrainingTask.ApplicationCore.Dto;
using TrainingTask.Common;
using TrainingTask.Infrastructure.Models;
using TrainingTask.Infrastructure.Repositories;

namespace TrainingTask.ApplicationCore.Repository
{
    public class ProjectService : IService<ProjectDto>
    {
        private readonly IRepository<Project> ProjectRepository;
        private readonly IConvert<Project, ProjectDto> ProjectDtoConverter;
        private readonly IConvert<ProjectDto, Project> ProjectConverter;
        private readonly ILogger logger;

        public ProjectService(IRepository<Project> projectRepository, IConvert<Project, ProjectDto> projectDtoConverter, IConvert<ProjectDto, Project> projectConverter, ILogger logger)
        {
            ProjectRepository = projectRepository;
            ProjectDtoConverter = projectDtoConverter;
            ProjectConverter = projectConverter;
            this.logger = logger;
        }

        public void Create(ProjectDto item)
        {
            logger.LogDebug(this.GetType() + ".Create is called");
            Project Project = ProjectConverter.Convert(item);
            ProjectRepository.Create(Project);
        }

        public void Delete(int id)
        {
            logger.LogDebug(this.GetType() + ".Delete is called");
            ProjectRepository.Delete(id);
        }

        public ProjectDto Get(int id)
        {
            logger.LogDebug(this.GetType() + ".Get is called");
            Project Project = ProjectRepository.Get(id);
            return ProjectDtoConverter.Convert(Project);
        }

        public IList<ProjectDto> GetAll()
        {
            logger.LogDebug(this.GetType() + ".GetAll is called");
            IList<Project> Projects = ProjectRepository.GetAll();
            IList<ProjectDto> ProjectsDto = ProjectDtoConverter.Convert(Projects);
            return ProjectsDto;
        }

        public void Update(ProjectDto item)
        {
            logger.LogDebug(this.GetType() + ".Update is called");
            Project Project = ProjectConverter.Convert(item);
            ProjectRepository.Update(Project);
        }
    }
}
