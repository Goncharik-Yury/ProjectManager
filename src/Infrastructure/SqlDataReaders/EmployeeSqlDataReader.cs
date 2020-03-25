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
        public EmployeeSqlDataReader(string connectionString) : base(connectionString)
        {
        }

        //protected override string ConnectionString { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        //public EmployeeSqlDataReader(string connectionString)
        //{
        //    this.connectionString = connectionString;
        //}

        //protected override string ConnectionString
        //{
        //    get => ConnectionString;
        //    set => ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\SeleSt\Programs\Projects\Database\TestTaskDB.mdf;Integrated Security=True;Connect Timeout=30";
        //}

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
