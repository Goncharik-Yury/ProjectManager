using System;
using System.Collections.Generic;
using System.Linq;
using TrainingTask.ApplicationCore.DTO;
using TrainingTask.Infrastructure.Models;

namespace TrainingTask.ApplicationCore.DBManipulators
{
    public static class DTOConverter
    {
        public static List<EmployeeDTO> EmployeeToDTO(List<Employee> employeesList)
        {
            List<EmployeeDTO> Converted = new List<EmployeeDTO>();
            foreach (var item in employeesList)
            {
                EmployeeDTO listItem = new EmployeeDTO
                {
                    Id = item.Id,
                    LastName = item.LastName,
                    FirstName = item.FirstName,
                    Patronymic = item.Patronymic,
                    Position = item.Position
                };

                Converted.Add(listItem);
            }

            return Converted;
        }

        public static List<ProjectDTO> ProjectToDTO(List<Project> projectsList)
        {
            List<ProjectDTO> Converted = new List<ProjectDTO>();

            foreach (var item in projectsList)
            {
                ProjectDTO listItem = new ProjectDTO
                {
                    Id = item.Id,
                    Name = item.Name,
                    ShortName = item.ShortName,
                    Description = item.Description
                };

                Converted.Add(listItem);
            }

            return Converted;
        }

        public static List<ProjectTaskDTO> ProjectTaskToDTO(List<ProjectTask> ProjectTasksList)
        {
            List<ProjectTaskDTO> Converted = new List<ProjectTaskDTO>();

            foreach (var item in ProjectTasksList)
            {
                ProjectTaskDTO listItem = new ProjectTaskDTO
                {
                    Id = item.Id,
                    Name = item.Name,
                    TimeToComplete = item.TimeToComplete,
                    BeginDate = item.BeginDate,
                    EndDate = item.EndDate,
                    Status = item.Status,
                    ExecutorId = item.ExecutorId
                };

                Converted.Add(listItem);
            }

            return Converted;
        }
    }
}
