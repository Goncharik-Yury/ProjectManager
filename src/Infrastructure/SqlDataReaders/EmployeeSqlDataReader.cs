using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using TrainingTask.Infrastructure.Models;

namespace TrainingTask.Infrastructure.SqlDataReaders
{
    public class EmployeeSqlDataReader : SqlDataReader<Employee>
    {
        public EmployeeSqlDataReader(string connectionString) : base(connectionString) { }

        protected override Employee DataParse(SqlDataReader sqlDataReader)
        {
            Employee employees = new Employee
            {
                Id = sqlDataReader.GetInt32("Id"),
                LastName = sqlDataReader.GetString("LastName"),
                FirstName = sqlDataReader.GetString("FirstName"),
                Patronymic = sqlDataReader.GetString("Patronymic"),
                Position = sqlDataReader.GetString("Position")
            };

            return employees;
        }
    }
}
