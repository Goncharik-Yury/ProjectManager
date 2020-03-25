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

        //private Employee GetRowItem(DataRow DataRow)
        //{
        //    Employee Employee = new Employee
        //    {
        //        Id = DataRow.Field<int>("Id"),
        //        LastName = DataRow.Field<string>("LastName"),
        //        FirstName = DataRow.Field<string>("FirstName"),
        //        Patronymic = DataRow.Field<string>("Patronymic"),
        //        Position = DataRow.Field<string>("Position")
        //    };

        //    return Employee;
        //}
    }
}
