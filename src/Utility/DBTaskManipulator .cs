using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Training_task.Models;
using Task = Training_task.Models.Task;

namespace Training_task.Utility
{

    public class DBTaskManipulator : DBManipulator
    {
        public List<Task> GetTasksList()
        {
            string SqlQueryString = "SELECT * FROM Task";
            return (List<Task>)DBGetData(SqlQueryString);
        }

        public bool CreateTask(Task task)
        {
            string SqlQueryString = $"INSERT INTO Task (Name, TimeToComplete, BeginDate, EndDate, Status) VALUES ('{task.Name}', '{task.TimeToComplete}', '{task.BeginDate}', '{task.EndDate}', '{task.Status}')";

            DBDoAction(SqlQueryString);
            return true;
        }

        public bool DeleteTask(int id)
        {
            string SqlQueryString = $"DELETE FROM Task WHERE id = {id}";

            DBDoAction(SqlQueryString);
            return true;
        }

        public bool EditTask(int id, Task task)
        {
            string SqlQueryString = $"UPDATE Task SET Name = '{task.Name}', TimeToComplete = '{task.TimeToComplete}', BeginDate = '{task.BeginDate}', EndDate = '{task.EndDate}', Status = '{task.Status}' WHERE id = {id}";

            DBDoAction(SqlQueryString);
            return true;
        }

        protected override object DataParse(SqlDataReader dataReader)
        {
            List<Task> tasksList = new List<Task>();
            while (dataReader.Read())
            {
                tasksList.Add(new Task(
                    (int)dataReader.GetValue(0),
                    (string)dataReader.GetValue(1),
                    (int)dataReader.GetValue(2),
                    (DateTime)dataReader.GetValue(3),
                    (DateTime)dataReader.GetValue(4),
                    dataReader.GetValue(5).ToString(),
                    (int)dataReader.GetValue(6)
                    ));
            }
            return tasksList;
        }
    }

}
