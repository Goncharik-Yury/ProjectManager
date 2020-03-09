using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Training_task.Models;
using Task = Training_task.Models.Task;

namespace Training_task.Utility
{
    public abstract class DBManipulator
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

        protected const string ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\SeleSt\Programs\Projects\training-task\TestTaskDatabase.mdf;Integrated Security=True;Connect Timeout=30";
        //const string ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\GoncharikYV\Documents\TestTaskDatabase.mdf;Integrated Security=True;Connect Timeout=30;Pooling = true";

        protected bool DBDoAction(string sqlQueryString)
        {
            using (SqlConnection DBConnection = new SqlConnection(ConnectionString))
            {
                try
                {
                    DBConnection.Open();
                    SqlCommand CommandToExecute = new SqlCommand(sqlQueryString, DBConnection);
                    CommandToExecute.ExecuteNonQuery();
                }
                catch { throw; }
            }
            return true;
        }

        protected object DBGetData(string sqlQueryString)
        {
            using (SqlConnection DBConnection = new SqlConnection(ConnectionString))
            {
                DBConnection.Open();
                SqlCommand CommandToExecute = new SqlCommand(sqlQueryString, DBConnection);
                SqlDataReader DataReader = CommandToExecute.ExecuteReader();

                return DataParse(DataReader);
            }
            //return new SqlDataReader();
        }

        protected abstract object DataParse(SqlDataReader dataReader);
    }
}
