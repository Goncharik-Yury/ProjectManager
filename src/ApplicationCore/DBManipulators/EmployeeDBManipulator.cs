using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using TrainingTask.ApplicationCore.DTO;
using TrainingTask.Infrastructure.Functional;
using TrainingTask.Infrastructure.Models;

namespace TrainingTask.ApplicationCore.DBManipulators
{
    public class EmployeeDBManipulator : DBManipulator
    {
        public List<EmployeeDTO> GetEmployeesList()
        {
            string SqlQueryString = $"SELECT * FROM Employee";

            return DTOConverter.EmployeeToDTO((List<Employee>)DBGetData(SqlQueryString));
        }

        public List<EmployeeDTO> GetEmployeeById(int id)
        {
            string SqlQueryString = $"SELECT * FROM Employee where Id = @Id";

            List<SqlParameter> QueryParameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id)
            };
            var Employees = (List<Employee>)DBGetData(SqlQueryString, QueryParameters);
            return DTOConverter.EmployeeToDTO(Employees);
        }

        // TODO: ? remove this method
        public List<EmployeeDTO> GetEmployeeByProjectTaskId(int projectTaskIdId)
        {
            string SqlQueryString = $"SELECT * FROM Employee where ProjectTaskId = @ProjectTaskId";

            List<SqlParameter> QueryParameters = new List<SqlParameter>
            {
                new SqlParameter("@ProjectTaskId", projectTaskIdId)
            };
            return DTOConverter.EmployeeToDTO((List<Employee>)DBGetData(SqlQueryString, QueryParameters));
        }

        public static bool CreateEmployee(EmployeeDTO employee)
        {
            string SqlQueryString = $"INSERT INTO Employee (LastName, FirstName, Patronymic , Position) VALUES (@LastName, @FirstName, @Patronymic, @Position)";
            List<SqlParameter> QueryParameters = new List<SqlParameter>
            {
                new SqlParameter("@LastName", employee.LastName),
                new SqlParameter("@FirstName", employee.FirstName),
                new SqlParameter("@Patronymic", employee.Patronymic),
                new SqlParameter("@Position", employee.Position)
            };

            DBDoAction(SqlQueryString, QueryParameters);
            return true;
        }

        public static bool DeleteEmployee(int id)
        {
            string SqlQueryString = $"DELETE FROM Employee WHERE id = @Id";
            List<SqlParameter> QueryParameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id)
            };
            DBDoAction(SqlQueryString, QueryParameters);
            return true;
        }

        public static bool EditEmployee(int id, EmployeeDTO employee)
        {
            string SqlQueryString = $"UPDATE Employee SET Lastname = @LastName, Firstname = @FirstName, Patronymic  = @Patronymic, Position = @Position WHERE id = @Id";
            List<SqlParameter> QueryParameters = new List<SqlParameter>
            {
                new SqlParameter("@LastName", employee.LastName),
                new SqlParameter("@FirstName", employee.FirstName),
                new SqlParameter("@Patronymic", employee.Patronymic),
                new SqlParameter("@Position", employee.Position),
                new SqlParameter("@Id", id)
            };
            DBDoAction(SqlQueryString, QueryParameters);
            return true;
        }

        // Working
        //protected override object DataParse(SqlDataReader dataReader)
        //{
        //    List<Employee> employees = new List<Employee>();
        //    while (dataReader.Read())
        //    {
        //        employees.Add(new Employee
        //        {
        //            Id = (int)dataReader.GetValue(0),
        //            LastName = dataReader.GetValue(1).ToString(),
        //            FirstName = dataReader.GetValue(2).ToString(),
        //            Patronymic = dataReader.GetValue(3).ToString(),
        //            Position = dataReader.GetValue(4).ToString()
        //        });
        //    }
        //    return employees;
        //}

        protected override object DataParse(DataTable dataTable)
        {
            List<Employee> employees = new List<Employee>();
            foreach (DataRow dr in dataTable.Rows)
            {
                employees.Add(new Employee
                {
                    Id = dr.Field<int>("Id"),
                    LastName = dr.Field<string>("LastName"),
                    FirstName = dr.Field<string>("FirstName"),
                    Patronymic = dr.Field<string>("Patronymic"),
                    Position = dr.Field<string>("Position")
                });
            }
            return employees;
        }
    }

}
