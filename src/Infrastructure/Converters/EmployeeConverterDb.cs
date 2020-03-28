using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using TrainingTask.Infrastructure.Models;

namespace TrainingTask.Infrastructure.Converters
{
    public class EmployeeConverterDb : IConvertDb<SqlDataReader, Employee>
    {
        public IList<Employee> Convert(SqlDataReader sqlDataReader)
        {
            List<Employee> Employees = new List<Employee>();
            while (sqlDataReader.Read())
            {
                Employee employee = new Employee
                {
                    Id = sqlDataReader.GetInt32("Id"),
                    LastName = sqlDataReader.GetString("LastName"),
                    FirstName = sqlDataReader.GetString("FirstName"),
                    Patronymic = sqlDataReader["Patronymic"] as string,
                    Position = sqlDataReader.GetString("Position")
                };

                Employees.Add(employee);
            }
            return Employees;
        }
    }
}
