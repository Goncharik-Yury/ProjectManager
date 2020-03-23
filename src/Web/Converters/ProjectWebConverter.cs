
using System.Collections.Generic;
using TrainingTask.ApplicationCore.Dto;
using TrainingTask.Web.ViewModels;

namespace TrainingTask.Web.Converters
{
    public class ProjectWebConverter : IConvertWeb<ProjectVm, ProjectDto>
    {
         public ProjectVm Convert(ProjectDto item)
        {
            return ConvertItem(item);
        }

        public ProjectDto Convert(ProjectVm item)
        {
            return ConvertItem(item);
        }

        public List<ProjectVm> ConvertAll(List<ProjectDto> items)
        {
            List<ProjectVm> ProjectsVm = new List<ProjectVm>();
            foreach (var item in items)
            {
                ProjectsVm.Add(ConvertItem(item));
            }

            return ProjectsVm;
        }

        public List<ProjectDto> ConvertList(List<ProjectVm> items)
        {
            List<ProjectDto> ProjectsDto = new List<ProjectDto>();
            foreach (var item in items)
            {
                ProjectsDto.Add(ConvertItem(item));
            }

            return ProjectsDto;
        }

        private ProjectDto ConvertItem(ProjectVm item)
        {
            return new ProjectDto
            {
                Id = item.Id,
                Name = item.Name,
                ShortName = item.ShortName,
                Description = item.Description
            };
        }

        private ProjectVm ConvertItem(ProjectDto item)
        {
            return new ProjectVm
            {
                Id = item.Id,
                Name = item.Name,
                ShortName = item.ShortName,
                Description = item.Description
            };
        }
    }
}
