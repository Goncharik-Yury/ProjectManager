using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Training_task.Models;

namespace Training_task.Utility
{
    public class DBManipulator
    {
        //private static DBManipulator instance;

        //private DBManipulator()
        //{ }

        //public static DBManipulator getInstance()
        //{
        //    if (instance == null)
        //        instance = new DBManipulator();
        //    return instance;
        //}

        const string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\GoncharikYV\Documents\TestTaskDatabase.mdf;Integrated Security=True;Connect Timeout=30";

        // TODO:Rewright
        public List<Employee> GetEmployeesList()
        {
            string sqlQueryString = "SELECT * FROM Employee";
            List<Employee> employees = new List<Employee>();

            using (SqlConnection DBConnection = new SqlConnection(connectionString))
            {

                DBConnection.Open();

                SqlCommand CommandToExecute = new SqlCommand(sqlQueryString, DBConnection);
                SqlDataReader dataReader = CommandToExecute.ExecuteReader();


                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        employees.Add(new Employee(
                            (int)dataReader.GetValue(0), dataReader.GetValue(1).ToString(), dataReader.GetValue(2).ToString(), dataReader.GetValue(3).ToString(), dataReader.GetValue(4).ToString())
                            );
                    }
                }
            }
            return employees;
        }

        // TODO:Rewright
        public bool CreateEmployee(Employee employee)
        {
            string sqlQueryString = $"INSERT INTO Employee (Id, LastName, FirstName, MiddleName, Position) VALUES ('5', '{employee.LastName}', '{employee.FirstName}', '{employee.MiddleName}', '{employee.Position}')";

            using (SqlConnection DBConnection = new SqlConnection(connectionString))
            {
                try
                {
                    DBConnection.Open();

                    SqlCommand CommandToExecute = new SqlCommand(sqlQueryString, DBConnection);
                    CommandToExecute.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("CreateEmployee exception:" + ex);
                }
            }
            return true;
        }
    }
}
