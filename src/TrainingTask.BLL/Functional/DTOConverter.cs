using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingTask.BLL.DTO;
using TrainingTask.DAL.Models;

namespace TrainingTask.BLL.Functional
{
    public static class DTOConverter
    {
        public static List<EmployeeDTO> EmployeeToDTO(List<Employee> employeesList)
        {
            List<EmployeeDTO> Converted = new List<EmployeeDTO>();
            //EmployeeDTO converted = new EmployeeDTO();
            foreach (var item in employeesList)
            {
                EmployeeDTO listItem = new EmployeeDTO();

                listItem.Id = item.Id;
                listItem.LastName = item.LastName;
                listItem.FirstName = item.FirstName;
                listItem.Patronymic = item.Patronymic;
                listItem.Position = item.Position;

                Converted.Add(listItem);
            }

            return Converted;
        }

        public static List<ProjectDTO> ProjectToDTO(List<Project> projectsList)
        {
            List<ProjectDTO> Converted = new List<ProjectDTO>();

            foreach (var item in projectsList)
            {
                ProjectDTO listItem = new ProjectDTO();

                listItem.Id = item.Id;
                listItem.Name = item.Name;
                listItem.ShortName = item.ShortName;
                listItem.Description = item.Description;

                Converted.Add(listItem);
            }

            return Converted;
        }

        public static List<ProjectTaskDTO> ProjectTaskToDTO(List<ProjectTask> ProjectTasksList)
        {
            List<ProjectTaskDTO> Converted = new List<ProjectTaskDTO>();

            foreach (var item in ProjectTasksList)
            {
                ProjectTaskDTO listItem = new ProjectTaskDTO();

                listItem.Id = item.Id;
                listItem.Name = item.Name;
                listItem.TimeToComplete = item.TimeToComplete;
                listItem.BeginDate = item.BeginDate;
                listItem.EndDate = item.EndDate;
                listItem.Status = item.Status;
                listItem.ExecutorId = item.ExecutorId;

                Converted.Add(listItem);
            }

            return Converted;
        }
    }
}
