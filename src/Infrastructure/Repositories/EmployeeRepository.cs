using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Linq;
using TrainingTask.Infrastructure.SqlDataReaders;
using TrainingTask.Infrastructure.Models;
using Microsoft.Extensions.Logging;
using TrainingTask.Common;

namespace TrainingTask.Infrastructure.Repositories
{
    public class EmployeeRepository : BaseRepository<Employee>, IRepository<Employee>
    {
        protected override string ConnectionString { get; }

        Func<SqlDataReader, List<Employee>> converter = ConvertToEmployee;
        public EmployeeRepository(string connectionString)
        {
            ConnectionString = connectionString;
        }

        static List<Employee> ConvertToEmployee(SqlDataReader sqlDataReader)
        {
            List<Employee> Employees = new List<Employee>();
            while (sqlDataReader.Read())
            {
                Employee employee = new Employee
                {
                    Id = sqlDataReader.GetInt32("Id"),
                    LastName = sqlDataReader.GetString("LastName"),
                    FirstName = sqlDataReader.GetString("FirstName"),
                    Patronymic = sqlDataReader.GetString("Patronymic"),
                    Position = sqlDataReader.GetString("Position")
                };

                Employees.Add(employee);
            }
            return Employees;
        }

        public void Create(Employee item)
        {
            string SqlQueryString = $"INSERT INTO Employee (LastName, FirstName, Patronymic , Position) VALUES (@LastName, @FirstName, @Patronymic, @Position)";
            List<SqlParameter> QueryParameters = new List<SqlParameter>
            {
                new SqlParameter("@LastName", item.LastName),
                new SqlParameter("@FirstName", item.FirstName),
                new SqlParameter("@Patronymic", item.Patronymic),
                new SqlParameter("@Position", item.Position)
            };

            ExecuteNonQuery(SqlQueryString, QueryParameters);
        }

        public void Delete(int id)
        {
            string SqlQueryString = $"DELETE FROM Employee WHERE id = @Id";
            List<SqlParameter> QueryParameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id)
            };

            ExecuteNonQuery(SqlQueryString, QueryParameters);
        }

        public Employee Get(int id)
        {
            string SqlQueryString = $"SELECT * FROM Employee where Id = @Id";

            List<SqlParameter> QueryParameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id)
            };

            IList<Employee> Employees = GetData(SqlQueryString, converter, QueryParameters);

            return Employees.FirstOrDefault();
        }

        public IList<Employee> GetAll()
        {
            string SqlQueryString = $"SELECT * FROM Employee";
            return GetData(SqlQueryString, converter, null);
        }

        public void Update(Employee item)
        {
            string SqlQueryString = $"UPDATE Employee SET LastName = @LastName, FirstName = @FirstName, Patronymic  = @Patronymic, Position = @Position WHERE id = @Id";
            List<SqlParameter> QueryParameters = new List<SqlParameter>
            {
                new SqlParameter("@LastName", item.LastName),
                new SqlParameter("@FirstName", item.FirstName),
                new SqlParameter("@Patronymic", item.Patronymic),
                new SqlParameter("@Position", item.Position),
                new SqlParameter("@Id", item.Id)
            };

            ExecuteNonQuery(SqlQueryString, QueryParameters);
        }
    }
}
