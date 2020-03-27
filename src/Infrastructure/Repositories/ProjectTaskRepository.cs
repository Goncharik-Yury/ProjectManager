using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using TrainingTask.Infrastructure.Models;
using System.Linq;
using TrainingTask.Common;

namespace TrainingTask.Infrastructure.Repositories
{
    public class ProjectTaskRepository : BaseRepository<ProjectTask>, IProjectTaskRepository<ProjectTask>
    {
        protected override string ConnectionString { get; }

        Func<SqlDataReader, List<ProjectTask>> converter = ConvertToProject;
        public ProjectTaskRepository(string connectionString)
        {
            ConnectionString = connectionString;
        }
        static List<ProjectTask> ConvertToProject(SqlDataReader sqlDataReader)
        {
            List<ProjectTask> ProjectTasks = new List<ProjectTask>();
            while (sqlDataReader.Read())
            {
                ProjectTask ProjectTask = new ProjectTask
                {
                    Id = sqlDataReader.GetInt32("Id"),
                    Name = sqlDataReader.GetString("Name"),
                    TimeToComplete = sqlDataReader.GetInt32("TimeToComplete"),
                    BeginDate = sqlDataReader.GetDateTime("BeginDate"),
                    EndDate = sqlDataReader.GetDateTime("EndDate"),
                    Status = sqlDataReader.GetString("Status"),
                    ProjectId = sqlDataReader.GetInt32("ProjectId"),
                    EmployeeId = sqlDataReader.GetInt32("EmployeeId")
                };

                ProjectTasks.Add(ProjectTask);
            }
            return ProjectTasks;
        }

        public void Create(ProjectTask item)
        {
            string SqlQueryString = $"INSERT INTO ProjectTask (Name, TimeToComplete, BeginDate, EndDate, Status, ProjectId, EmployeeId) VALUES (@Name, @TimeToComplete, @BeginDate, @EndDate, @Status, @ProjectId, @EmployeeId)";
            List<SqlParameter> QueryParameters = GetEntityParameters(item);


            ExecuteNonQuery(SqlQueryString, QueryParameters);
        }

        public void Delete(int id)
        {
            string SqlQueryString = $"DELETE FROM ProjectTask WHERE id = @Id";
            List<SqlParameter> QueryParameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id)
            };

            ExecuteNonQuery(SqlQueryString, QueryParameters);
        }

        public IList<ProjectTask> GetAll()
        {
            string SqlQueryString = "SELECT * FROM ProjectTask";
            IList<ProjectTask> ProjectTasksList = GetData(SqlQueryString, converter, null);

            return ProjectTasksList;
        }

        public IList<ProjectTask> GetAllByProjectId(int id)
        {
            string SqlQueryString = $"SELECT * FROM ProjectTask WHERE ProjectId = @ProjectId";
            List<SqlParameter> QueryParameters = new List<SqlParameter>
            {
                new SqlParameter("@ProjectId", id)
            };

            IList<ProjectTask> Projects = GetData(SqlQueryString, converter, QueryParameters);

            return Projects;
        }

        public ProjectTask Get(int id)
        {
            string SqlQueryString = $"SELECT * FROM ProjectTask WHERE Id = @Id";
            List<SqlParameter> QueryParameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id)
            };
            ProjectTask ProjectTask = GetData(SqlQueryString, converter, QueryParameters).FirstOrDefault();

            return ProjectTask;
        }

        public void Update(ProjectTask item)
        {
            string SqlQueryString = $"UPDATE ProjectTask SET Name = @Name, TimeToComplete = @TimeToComplete, BeginDate = @BeginDate, EndDate = @EndDate, Status = @Status, EmployeeId = @EmployeeId, ProjectId = @ProjectId WHERE Id = @Id";
            List<SqlParameter> QueryParameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", item.Id)
            };
            QueryParameters.AddRange(GetEntityParameters(item));

            ExecuteNonQuery(SqlQueryString, QueryParameters);
        }

        private List<SqlParameter> GetEntityParameters(ProjectTask item)
        {
            return new List<SqlParameter>
            {
                new SqlParameter("@Name", item.Name),
                new SqlParameter("@TimeToComplete", item.TimeToComplete),
                new SqlParameter("@BeginDate", item.BeginDate),
                new SqlParameter("@EndDate", item.EndDate),
                new SqlParameter("@Status", item.Status),
                new SqlParameter("@ProjectId", item.ProjectId),
                new SqlParameter("@EmployeeId", item.EmployeeId)
            };
        }
    }
}
