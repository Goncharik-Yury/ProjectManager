using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Linq;
using TrainingTask.Infrastructure.Models;

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
                    Patronymic = sqlDataReader["Patronymic"] as string,
                    Position = sqlDataReader.GetString("Position")
                };

                Employees.Add(employee);
            }
            return Employees;
        }

        public void Create(Employee item)
        {
            string SqlQueryString = $"INSERT INTO Employee (LastName, FirstName, Patronymic , Position) VALUES (@LastName, @FirstName, @Patronymic, @Position)";
            ExecuteNonQuery(SqlQueryString, GetCreateParameters(item));
        }

        public void Delete(int id)
        {
            string SqlQueryString = $"DELETE FROM Employee WHERE id = @Id";
            ExecuteNonQuery(SqlQueryString, GetIdParameter(id));
        }

        public Employee Get(int id)
        {
            string SqlQueryString = $"SELECT * FROM Employee where Id = @Id";
            IList<Employee> Employees = GetData(SqlQueryString, converter, GetIdParameter(id));
            return Employees.FirstOrDefault();
        }

        public IList<Employee> GetAll()
        {
            string SqlQueryString = $"SELECT * FROM Employee";
            return GetData(SqlQueryString, converter);
        }

        public void Update(Employee item)
        {
            string SqlQueryString = $"UPDATE Employee SET LastName = @LastName, FirstName = @FirstName, Patronymic  = @Patronymic, Position = @Position WHERE id = @Id";
            ExecuteNonQuery(SqlQueryString, GetUpdateParameters(item));
        }

        private List<SqlParameter> GetCreateParameters(Employee item)
        {
            return new List<SqlParameter>
            {
                new SqlParameter("@LastName", item.LastName),
                new SqlParameter("@FirstName", item.FirstName),
                new SqlParameter("@Patronymic", SqlDbType.NVarChar) {Value = item.Patronymic ?? (object)DBNull.Value},
                new SqlParameter("@Position", item.Position)
            };
        }

        private List<SqlParameter> GetUpdateParameters(Employee item)
        {
            List<SqlParameter> SqlParameters = GetCreateParameters(item);
            SqlParameters.AddRange(GetIdParameter(item.Id));
            return SqlParameters;
        }

        private List<SqlParameter> GetIdParameter(int id)
        {
            List<SqlParameter> QueryParameters = new List<SqlParameter>();
            QueryParameters.Add(new SqlParameter("@Id", id));
            return QueryParameters;
        }
    }
}
