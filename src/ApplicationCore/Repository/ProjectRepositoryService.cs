using ApplicationCore.Converters;
using System;
using System.Collections.Generic;
using System.Text;
using TrainingTask.ApplicationCore.Dto;
using TrainingTask.Infrastructure.Models;
using TrainingTask.Infrastructure.Repositories;

namespace ApplicationCore.Repository
{
    public class ProjectRepositoryService : IRepositoryService<ProjectDto>
    {
        IRepository<Project> Repository;
        IConvertBll<Project, ProjectDto> Converter;

        public ProjectRepositoryService()
        {
            Repository = new ProjectRepository();
            Converter = new ProjectBloConverter();
        }

        public void Create(ProjectDto item)
        {
            Project Project = Converter.Convert(item);
            Repository.Create(Project);
        }

        public void Delete(int id)
        {
            Repository.Delete(id);
        }

        public ProjectDto GetSingle(int id)
        {
            Project Project = Repository.GetSingle(id);
            return Converter.Convert(Project);
        }

        public List<ProjectDto> GetAll()
        {
            List<Project> Projects = Repository.GetAll();
            List<ProjectDto> ProjectsDto = Converter.ConvertList(Projects);
            return ProjectsDto;
        }

        public void Update(ProjectDto item)
        {
            Project Project = Converter.Convert(item);
            Repository.Update(Project);
        }
    }
}
