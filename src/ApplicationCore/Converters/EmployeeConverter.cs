using System;
using System.Collections.Generic;
using System.Text;
using TrainingTask.ApplicationCore.Dto;
using TrainingTask.Common;
using TrainingTask.Infrastructure.Models;

namespace TrainingTask.ApplicationCore.Converters
{
    public class EmployeeConverter : IConvert<EmployeeDto, Employee>
    {
        public Employee Convert(EmployeeDto item)
        {
            return new Employee
            {
                Id = item.Id,
                LastName = item.LastName,
                FirstName = item.FirstName,
                Patronymic = item.Patronymic,
                Position = item.Position
            };
        }
        public IList<Employee> Convert(IList<EmployeeDto> items)
        {
            List<Employee> Employees = new List<Employee>();
            foreach (var item in items)
            {
                Employees.Add(Convert(item));
            }

            return Employees;
        }
    }
}
