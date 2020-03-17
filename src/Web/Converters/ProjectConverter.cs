using System;
using System.Collections.Generic;
using System.Linq;
using TrainingTask.ApplicationCore.DTO;
using TrainingTask.Web.Models;

namespace TrainingTask.Web.Converters
{
    public static class ProjectConverter
    {
        public static List<ProjectViewModel> DTOtoViewModelList(List<ProjectDTO> convert)
        {
            List<ProjectViewModel> converted = new List<ProjectViewModel>();
            foreach (var item in convert)
            {
                ProjectViewModel listItem = new ProjectViewModel
                {
                    Id = item.Id,
                    Name = item.Name,
                    ShortName = item.ShortName,
                    Description = item.Description
                };
                converted.Add(listItem);
            }

            return converted;
        }
        //public static ProjectViewModel DTOtoViewModel(ProjectDTO convert)
        //{
        //    ProjectViewModel converted = new ProjectViewModel
        //    {
        //        Id = convert.Id,
        //        Name = convert.Name,
        //        ShortName = convert.ShortName,
        //        Description = convert.Description
        //    };

        //    return converted;
        //}

        public static ProjectDTO ViewModelToDTO(ProjectViewModel convert)
    {
        ProjectDTO Project = new ProjectDTO
        {
            Id = convert.Id,
            Name = convert.Name,
            ShortName = convert.ShortName,
            Description = convert.Description
        };

        return Project;
    }
}
}
