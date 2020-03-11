using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using TrainingTask.BLL.DTO;
using TrainingTask.DAL.Functional;
using TrainingTask.DAL.Models;

namespace TrainingTask.BLL.Functional
{
    public class DBEmployeeManipulator : DBManipulator
    {
        public List<EmployeeDTO> GetEmployeesList()
        {
            string SqlQueryString = $"SELECT * FROM Employee";

            return DTOConverter.EmployeeToDTO((List<Employee>)DBGetData(SqlQueryString));
        }

        public List<EmployeeDTO> GetEmployeeById(int id)
        {
            string SqlQueryString = $"SELECT * FROM Employee where Id = {id}";
            return DTOConverter.EmployeeToDTO((List<Employee>)DBGetData(SqlQueryString));
        }

        public static bool CreateEmployee(EmployeeDTO employee)
        {
            string SqlQueryString = $"INSERT INTO Employee (LastName, FirstName, Patronymic , Position) VALUES ('{employee.LastName}', '{employee.FirstName}', '{employee.Patronymic }', '{employee.Position}')";

            DBDoAction(SqlQueryString);
            return true;
        }

        public static bool DeleteEmployee(int id)
        {
            string SqlQueryString = $"DELETE FROM Employee WHERE id = {id}";

            DBDoAction(SqlQueryString);
            return true;
        }

        public static bool EditEmployee(int id, EmployeeDTO employee)
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
                employees.Add(new Employee
                {
                    Id = (int)dataReader.GetValue(0),
                    LastName = dataReader.GetValue(1).ToString(),
                    FirstName = dataReader.GetValue(2).ToString(),
                    Patronymic = dataReader.GetValue(3).ToString(),
                    Position = dataReader.GetValue(4).ToString()
                });
            }
            return employees;
        }

        //protected override object DataParseList(SqlDataReader dataReader)
        //{
        //    List<Employee> employees = new List<Employee>();
        //    while (true)
        //    {
        //        employees.Add((Employee)DataParseSingle(dataReader));
        //    }
        //    return employees;
        //}

        //protected override object DataParseSingle(SqlDataReader dataReader)
        //{
        //    dataReader.Read();
        //    return new Employee(
        //            (int)dataReader.GetValue(0),
        //            dataReader.GetValue(1).ToString(),
        //            dataReader.GetValue(2).ToString(),
        //            dataReader.GetValue(3).ToString(),
        //            dataReader.GetValue(4).ToString()
        //            );
        //}
    }

}
