using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Training_task.Models;

namespace Training_task.Utility
{
    public class DBEmployeeManipulator : DBManipulator
    {
        public List<Employee> GetEmployeesList()
        {
            string SqlQueryString = $"SELECT * FROM Employee";
            return (List<Employee>)DBGetData(SqlQueryString);
        }

        public List<Employee> GetEmployeeById(int id)
        {
            string SqlQueryString = $"SELECT * FROM Employee where Id = {id}";
            return (List<Employee>)DBGetData(SqlQueryString);
        }

        public bool CreateEmployee(Employee employee)
        {
            string SqlQueryString = $"INSERT INTO Employee (LastName, FirstName, Patronymic , Position) VALUES ('{employee.LastName}', '{employee.FirstName}', '{employee.Patronymic }', '{employee.Position}')";

            DBDoAction(SqlQueryString);
            return true;
        }

        public bool DeleteEmployee(int id)
        {
            string SqlQueryString = $"DELETE FROM Employee WHERE id = {id}";

            DBDoAction(SqlQueryString);
            return true;
        }

        public bool EditEmployee(int id, Employee employee)
        {
            string SqlQueryString = $"UPDATE Employee SET Lastname = '{employee.LastName}', Firstname = '{employee.FirstName}', Patronymic  = '{employee.Patronymic }', Position = '{employee.Position}' WHERE id = {id}";

            DBDoAction(SqlQueryString);
            return true;
        }

        protected override object DataParse(SqlDataReader dataReader)
        {
            List<Employee> employees = new List<Employee>();
            while (dataReader.Read())
            {
                employees.Add(new Employee(
                    (int)dataReader.GetValue(0),
                    dataReader.GetValue(1).ToString(),
                    dataReader.GetValue(2).ToString(),
                    dataReader.GetValue(3).ToString(),
                    dataReader.GetValue(4).ToString()
                    ));
            }
            return employees;
        }
    }

}
