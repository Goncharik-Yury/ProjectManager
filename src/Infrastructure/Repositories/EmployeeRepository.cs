using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Linq;
using ProjectManager.Infrastructure.Models;
using Microsoft.Extensions.Logging;
using ProjectManager.Infrastructure.Converters;

namespace ProjectManager.Infrastructure.Repositories
{
    public class EmployeeRepository : BaseRepository<Employee>, IRepository<Employee>
    {

        protected override string ConnectionString { get; }
        private readonly Func<SqlDataReader, IList<Employee>> employeeConverterDelegate;

        public EmployeeRepository(string connectionString, IConvertDb<SqlDataReader, Employee> employeeConverter, ILogger logger) : base(logger)
        {
            ConnectionString = connectionString;
            employeeConverterDelegate = employeeConverter.Convert;
        }

        public void Create(Employee item)
        {
            logger.LogDebug(GetType() + ".Create is called");
            string SqlQueryString = $"INSERT INTO Employee (LastName, FirstName, Patronymic , Position) VALUES (@LastName, @FirstName, @Patronymic, @Position)";
            ExecuteNonQuery(SqlQueryString, GetCreateParameters(item));
        }

        public void Delete(int id)
        {
            logger.LogDebug(GetType() + ".Delete is called");
            string SqlQueryString = $"DELETE FROM Employee WHERE id = @Id";
            ExecuteNonQuery(SqlQueryString, GetIdParameter(id));
        }

        public Employee GetSingle(int id)
        {
            logger.LogDebug(GetType() + ".Get is called");
            string SqlQueryString = $"SELECT * FROM Employee where Id = @Id";
            IList<Employee> Employees = GetData(SqlQueryString, employeeConverterDelegate, GetIdParameter(id));
            return Employees.FirstOrDefault();
        }

        public IList<Employee> GetAll()
        {
            logger.LogDebug(GetType() + ".GetAll is called");
            string SqlQueryString = $"SELECT * FROM Employee";
            return GetData(SqlQueryString, employeeConverterDelegate);
        }

        public void Update(Employee item)
        {
            logger.LogDebug(GetType() + ".Update is called");
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
