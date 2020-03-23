using System;
using System.Collections.Generic;
using System.Text;
using TrainingTask.ApplicationCore.Dto;
using TrainingTask.Infrastructure.Models;

namespace ApplicationCore.Converters
{
    class ProjectBloConverter : IConvertBll<Project, ProjectDto>
    {
        public Project Convert(ProjectDto item)
        {
            return ConvertItem(item);
        }

        public ProjectDto Convert(Project item)
        {
            ProjectDto ProjectDto = ConvertItem(item);

            return ProjectDto;
        }

        public List<Project> ConvertList(List<ProjectDto> items)
        {
            List<Project> Projects = new List<Project>();
            foreach (var item in items)
            {
                Projects.Add(ConvertItem(item));
            }

            return Projects;
        }

        public List<ProjectDto> ConvertList(List<Project> items)
        {
            List<ProjectDto> ProjectsDto = new List<ProjectDto>();
            foreach (var item in items)
            {
                ProjectsDto.Add(ConvertItem(item));
            }

            return ProjectsDto;
        }
        private Project ConvertItem(ProjectDto item)
        {
            return new Project
            {
                Id = item.Id,
                Name = item.Name,
                ShortName = item.ShortName,
                Description = item.Description
            };
        }
        private ProjectDto ConvertItem(Project item)
        {
            return new ProjectDto
            {
                Id = item.Id,
                Name = item.Name,
                ShortName = item.ShortName,
                Description = item.Description
            };
        }
    }
}
