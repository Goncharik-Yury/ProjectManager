using System;
using System.Collections.Generic;
using TrainingTask.ApplicationCore.Dto;
using TrainingTask.Common;
using TrainingTask.Web.ViewModels;

namespace TrainingTask.Web.Converters
{
    public class ProjectVmConverter : IConvert<ProjectDto, ProjectVm>
    {
        public ProjectVm Convert(ProjectDto item)
        {
            return new ProjectVm
            {
                Id = item.Id,
                Name = item.Name,
                ShortName = item.ShortName,
                Description = item.Description
            };
        }

        public IList<ProjectVm> Convert(IList<ProjectDto> items)
        {
            List<ProjectVm> ProjectsDto = new List<ProjectVm>();
            foreach (var item in items)
            {
                ProjectsDto.Add(Convert(item));
            }

            return ProjectsDto;
        }
    }
}
