using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using TrainingTask.Infrastructure.Converters;
using TrainingTask.Infrastructure.DbOperators;
using TrainingTask.Infrastructure.Models;

namespace TrainingTask.Infrastructure.Repositories
{
    public class EmployeeRepository : IRepository<Employee>
    {
        IDbOperator DbOperator;
        IConvertDal<Employee, DataTable> Converter;

        public EmployeeRepository()
        {
            DbOperator = new DbOperator();
            Converter = new EmployeeDalConverter();
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

            DbOperator.ExecuteNonQuery(SqlQueryString, QueryParameters);
        }

        public void Delete(int id)
        {
            string SqlQueryString = $"DELETE FROM Employee WHERE id = @Id";
            List<SqlParameter> QueryParameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id)
            };
            DbOperator.ExecuteNonQuery(SqlQueryString, QueryParameters);
        }

        public Employee GetSingle(int id)
        {
            string SqlQueryString = $"SELECT * FROM Employee where Id = @Id";

            List<SqlParameter> QueryParameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id)
            };
            Employee Employees = Converter.Convert(DbOperator.GetData(SqlQueryString, QueryParameters));
            return Employees;
        }

        public List<Employee> GetAll()
        {
            string SqlQueryString = $"SELECT * FROM Employee";
            return Converter.ConvertAll(DbOperator.GetData(SqlQueryString));
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
            DbOperator.ExecuteNonQuery(SqlQueryString, QueryParameters);
        }
    }
}
