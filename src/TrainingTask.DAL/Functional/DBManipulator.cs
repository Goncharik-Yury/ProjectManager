using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace TrainingTask.DAL.Functional
{
    public abstract class DBManipulator
    {
        private static string GetConnectionString()
        {
            SqlConnectionStringBuilder builder =
            new SqlConnectionStringBuilder();

            builder.DataSource = @"(LocalDB)\MSSQLLocalDB";
            builder.AttachDBFilename = @"C:\SeleSt\Programs\Projects\Database\TestTaskDatabase.mdf";
            builder.IntegratedSecurity = true;
            builder.ConnectTimeout = 30;

            return builder.ToString();
        }

        protected bool DBDoAction(string sqlQueryString)
        {
            using (SqlConnection DBConnection = new SqlConnection(GetConnectionString()))
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
            using (SqlConnection DBConnection = new SqlConnection(GetConnectionString()))
            {
                DBConnection.Open();
                SqlCommand CommandToExecute = new SqlCommand(sqlQueryString, DBConnection);
                SqlDataReader DataReader = CommandToExecute.ExecuteReader();

                return DataParse(DataReader);
            }
        }

        protected abstract object DataParse(SqlDataReader dataReader);
        //protected abstract object DataParseList(SqlDataReader dataReader);
    }
}
