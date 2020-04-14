using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using ProjectManager.Infrastructure.Models;

namespace ProjectManager.Infrastructure.Converters
{
    public class ProjectTaskConverterDb : IConvertDb<SqlDataReader, ProjectTask>
    {
        public IList<ProjectTask> Convert(SqlDataReader sqlDataReader)
        {
            List<ProjectTask> ProjectTasks = new List<ProjectTask>();
            while (sqlDataReader.Read())
            {
                ProjectTask projectTask = new ProjectTask
                {
                    Id = sqlDataReader.GetInt32("Id"),
                    Name = sqlDataReader.GetString("Name"),
                    TimeToComplete = sqlDataReader["TimeToComplete"] as int?,
                    BeginDate = sqlDataReader["BeginDate"] as DateTime?,
                    EndDate = sqlDataReader["EndDate"] as DateTime?,
                    Status = sqlDataReader.GetString("Status"),
                    ProjectId = sqlDataReader.GetInt32("ProjectId"),
                    EmployeeId = sqlDataReader.GetInt32("EmployeeId")
                };


                ProjectTasks.Add(projectTask);
            }
            return ProjectTasks;
        }
    }
}
