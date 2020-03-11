using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using TrainingTask.ApplicationCore.DTO;
using TrainingTask.Infrastructure.Functional;
using TrainingTask.Infrastructure.Models;

namespace TrainingTask.ApplicationCore.DBManipulators
{
    public class DBProjectTaskManipulator : DBManipulator
    {
        public List<ProjectTaskDTO> GetProjectTasksList()
        {
            string SqlQueryString = "SELECT * FROM ProjectTask";
            var ProjectTasksList = (List<ProjectTask>)DBGetData(SqlQueryString);
            return DTOConverter.ProjectTaskToDTO(ProjectTasksList);
        }
        public List<ProjectTaskDTO> GetProjectTasksbyId(int id)
        {
            string SqlQueryString = $"SELECT * FROM ProjectTask WHERE Id = {id}";
            var ProjectTasksList = (List<ProjectTask>)DBGetData(SqlQueryString);
            return DTOConverter.ProjectTaskToDTO(ProjectTasksList);
        }

        public static bool CreateProjectTask(ProjectTaskDTO projectTask)
        {
            string SqlQueryString = $"INSERT INTO ProjectTask (Name, TimeToComplete, BeginDate, EndDate, Status, ExecutorId) VALUES ('{projectTask.Name}', '{projectTask.TimeToComplete}', '{projectTask.BeginDate}', '{projectTask.EndDate}', '{projectTask.Status}', '{projectTask.ExecutorId}')";

            DBDoAction(SqlQueryString);
            return true;
        }

        public static bool DeleteProjectTask(int id)
        {
            string SqlQueryString = $"DELETE FROM ProjectTask WHERE id = {id}";

            DBDoAction(SqlQueryString);
            return true;
        }

        public static bool EditProjectTask(int id, ProjectTaskDTO task)
        {
            string SqlQueryString = $"UPDATE Task SET Name = '{task.Name}', TimeToComplete = '{task.TimeToComplete}', BeginDate = '{task.BeginDate}', EndDate = '{task.EndDate}', Status = '{task.Status}' WHERE id = {id}";

            DBDoAction(SqlQueryString);
            return true;
        }

        protected override object DataParse(SqlDataReader dataReader)
        {
            List<ProjectTask> tasksList = new List<ProjectTask>();
            while (dataReader.Read())
            {
                tasksList.Add(new ProjectTask()
                {
                    Id = (int)dataReader.GetValue(0),
                    Name = (string)dataReader.GetValue(1),
                    TimeToComplete = (int)dataReader.GetValue(2),
                    BeginDate = (DateTime)dataReader.GetValue(3),
                    EndDate = (DateTime)dataReader.GetValue(4),
                    Status = dataReader.GetValue(5).ToString(),
                    ExecutorId = (int)dataReader.GetValue(6)
                });
            }
            return tasksList;
        }
    }

}
