using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingTask.BLL.DTO;
using TrainingTask.WEB.Models;

namespace TrainingTask.WEB.Functional
{
    public static class ViewModelConverter
    {
        public static List<EmployeeViewModel> EmployeeDTOtoViewModel(List<EmployeeDTO> employee)
        {
            List<EmployeeViewModel> converted = new List<EmployeeViewModel>();
            foreach (var item in employee)
            {
                EmployeeViewModel listItem = new EmployeeViewModel();

                listItem.Id = item.Id;
                listItem.LastName = item.LastName;
                listItem.FirstName = item.FirstName;
                listItem.Patronymic = item.Patronymic;
                listItem.Position = item.Position;

                converted.Add(listItem);
            }

            return converted;
        }

        public static List<ProjectViewModel> ProjectDTOtoViewModel(List<ProjectDTO> project)
        {
            List<ProjectViewModel> converted = new List<ProjectViewModel>();
            foreach (var item in project)
            {
                ProjectViewModel listItem = new ProjectViewModel();

                listItem.Id = item.Id;
                listItem.Name = item.Name;
                listItem.ShortName = item.ShortName;
                listItem.Description = item.Description;

                converted.Add(listItem);
            }

            return converted;
        }

        public static List<ProjectTaskViewModel> ProjectTaskDTOtoViewModel(List<ProjectTaskDTO> projectTask)
        {
            List<ProjectTaskViewModel> converted = new List<ProjectTaskViewModel>();
            foreach (var item in projectTask)
            {
                ProjectTaskViewModel listItem = new ProjectTaskViewModel();

                listItem.Id = item.Id;
                listItem.Name = item.Name;
                listItem.TimeToComplete = item.TimeToComplete;
                listItem.BeginDate = item.BeginDate;
                listItem.EndDate = item.EndDate;
                listItem.Status = item.Status;
                listItem.ExecutorId = item.ExecutorId;

                converted.Add(listItem);
            }

            return converted;
        }
    }
}
