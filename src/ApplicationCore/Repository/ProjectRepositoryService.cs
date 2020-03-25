using System;
using System.Collections.Generic;
using System.Text;
using TrainingTask.ApplicationCore.Dto;
using TrainingTask.Common;
using TrainingTask.Infrastructure.Models;
using TrainingTask.Infrastructure.Repositories;

namespace TrainingTask.ApplicationCore.Repository
{
    public class ProjectRepositoryService : IRepositoryService<ProjectDto>
    {
        IRepository<Project> ProjectRepository;
        IConvert<Project, ProjectDto> ProjectDtoConverter;
        IConvert<ProjectDto, Project> ProjectConverter;

        public ProjectRepositoryService(IRepository<Project> projectRepository, IConvert<Project, ProjectDto> projectDtoConverter, 
            IConvert<ProjectDto, Project> projectConverter
            )
        {
            ProjectRepository = projectRepository;
            ProjectDtoConverter = projectDtoConverter;
            ProjectConverter = projectConverter;
        }

        public void Create(ProjectDto item)
        {
            Project Project = ProjectConverter.Convert(item);
            ProjectRepository.Create(Project);
        }

        public void Delete(int id)
        {
            ProjectRepository.Delete(id);
        }

        public ProjectDto GetSingle(int id)
        {
            Project Project = ProjectRepository.GetSingle(id);
            return ProjectDtoConverter.Convert(Project);
        }

        public IList<ProjectDto> GetAll()
        {
            IList<Project> Projects = ProjectRepository.GetAll();
            IList<ProjectDto> ProjectsDto = ProjectDtoConverter.ConvertAll(Projects);
            return ProjectsDto;
        }

        public void Update(ProjectDto item)
        {
            Project Project = ProjectConverter.Convert(item);
            ProjectRepository.Update(Project);
        }
    }
}
