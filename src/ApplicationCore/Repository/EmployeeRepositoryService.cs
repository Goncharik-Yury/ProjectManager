using ApplicationCore.Converters;
using System;
using System.Collections.Generic;
using System.Text;
using TrainingTask.ApplicationCore.Dto;
using TrainingTask.Infrastructure.Models;
using TrainingTask.Infrastructure.Repositories;

namespace ApplicationCore.Repository
{
    public class EmployeeRepositoryService : IRepositoryService<EmployeeDto>
    {
        IRepository<Employee> Repository;
        IConvertBll<Employee, EmployeeDto> Converter;
        public EmployeeRepositoryService()
        {
            Repository = new EmployeeRepository();
            Converter = new EmployeeBloConverter();
        }

        public void Create(EmployeeDto item)
        {
            Employee Employee = Converter.Convert(item);
            Repository.Create(Employee);
        }

        public void Delete(int id)
        {
            Repository.Delete(id);
        }

        public EmployeeDto GetSingle(int id)
        {
            Employee Employee = Repository.GetSingle(id);
            EmployeeDto EmployeeDto = Converter.Convert(Employee);
            return EmployeeDto;
        }

        public List<EmployeeDto> GetAll()
        {
            List<Employee> Employees = Repository.GetAll();
            List<EmployeeDto> EmployeesDto = new List<EmployeeDto>();
            foreach (var item in Employees)
            {
                EmployeesDto.Add(Converter.Convert(item));
            }
            return EmployeesDto;
        }

        public void Update(EmployeeDto item)
        {
            Employee Employee = Converter.Convert(item);
            Repository.Update(Employee);
        }
    }
}
