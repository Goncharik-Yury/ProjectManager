using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using TrainingTask.Infrastructure.Converters;
using System.Linq;
using TrainingTask.Infrastructure.SqlDataReaders;
using TrainingTask.Infrastructure.Models;

namespace TrainingTask.Infrastructure.Repositories
{
    public class EmployeeRepository : IRepository<Employee>
    {
        private readonly ISqlDataReader<Employee> EmployeeSqlDataReader;

        public EmployeeRepository(ISqlDataReader<Employee> employeeSqlDataReader)
        {
            EmployeeSqlDataReader = employeeSqlDataReader;
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

            EmployeeSqlDataReader.ExecuteNonQuery(SqlQueryString, QueryParameters);
        }

        public void Delete(int id)
        {
            string SqlQueryString = $"DELETE FROM Employee WHERE id = @Id";
            List<SqlParameter> QueryParameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id)
            };

            EmployeeSqlDataReader.ExecuteNonQuery(SqlQueryString, QueryParameters);
        }

        public Employee Get(int id)
        {
            string SqlQueryString = $"SELECT * FROM Employee where Id = @Id";

            List<SqlParameter> QueryParameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id)
            };

            IList<Employee> Employees = EmployeeSqlDataReader.GetData(SqlQueryString, QueryParameters);
 
            return Employees.FirstOrDefault();
        }

        public IList<Employee> GetAll()
        {
            string SqlQueryString = $"SELECT * FROM Employee";
            return EmployeeSqlDataReader.GetData(SqlQueryString);
        }

        public void Update(Employee item)
        {
            string SqlQueryString = $"UPDATE Employee SET Lastname = @LastName, Firstname = @FirstName, Patronymic  = @Patronymic, Position = @Position WHERE id = @Id";
            List<SqlParameter> QueryParameters = new List<SqlParameter>
            {
                new SqlParameter("@LastName", item.LastName),
                new SqlParameter("@FirstName", item.FirstName),
                new SqlParameter("@Patronymic", item.Patronymic),
                new SqlParameter("@Position", item.Position),
                new SqlParameter("@Id", item.Id)
            };

            EmployeeSqlDataReader.ExecuteNonQuery(SqlQueryString, QueryParameters);
        }
    }
}
