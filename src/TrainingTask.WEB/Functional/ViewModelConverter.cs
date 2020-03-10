using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingTask.BLL.DTO;
using TrainingTask.WEB.Models;

namespace TrainingTask.WEB.Functional
{
    public static class ConverterViewModel
    {
        public static List<EmployeeViewModel> EmployeeDTOtoViewModel(List<EmployeeDTO> convert)
        {
            List<EmployeeViewModel> converted = new List<EmployeeViewModel>();
            foreach (var item in convert)
            {
                EmployeeViewModel listItem = new EmployeeViewModel
                {
                    Id = item.Id,
                    LastName = item.LastName,
                    FirstName = item.FirstName,
                    Patronymic = item.Patronymic,
                    Position = item.Position
                };

                converted.Add(listItem);
            }

            return converted;
        }

        public static EmployeeDTO EmployeeViewModelToDTO(EmployeeViewModel convert)
        {
            EmployeeDTO converted = new EmployeeDTO
            {
                Id = convert.Id,
                LastName = convert.LastName,
                FirstName = convert.FirstName,
                Patronymic = convert.Patronymic,
                Position = convert.Position
            };

            return converted;
        }

        public static List<ProjectViewModel> ProjectDTOtoViewModel(List<ProjectDTO> convert)
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

        public static ProjectDTO ProjectViewModelToDTO(ProjectViewModel convert)
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

        public static List<ProjectTaskViewModel> ProjectTaskDTOtoViewModel(List<ProjectTaskDTO> convert)
        {
            List<ProjectTaskViewModel> converted = new List<ProjectTaskViewModel>();
            foreach (var item in convert)
            {
                ProjectTaskViewModel listItem = new ProjectTaskViewModel
                {
                    Id = item.Id,
                    Name = item.Name,
                    TimeToComplete = item.TimeToComplete,
                    BeginDate = item.BeginDate,
                    EndDate = item.EndDate,
                    Status = item.Status,
                    ExecutorId = item.ExecutorId
                };

                converted.Add(listItem);
            }

            return converted;
        }

        public static ProjectTaskDTO ProjectTaskViewModelToDTO(ProjectTaskViewModel convert)
        {
            ProjectTaskDTO converted = new ProjectTaskDTO
            {
                Id = convert.Id,
                Name = convert.Name,
                TimeToComplete = convert.TimeToComplete,
                BeginDate = convert.BeginDate,
                EndDate = convert.EndDate,
                Status = convert.Status,
                ExecutorId = convert.ExecutorId
            };

            return converted;
        }
    }
}
