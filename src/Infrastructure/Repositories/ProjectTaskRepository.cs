using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using TrainingTask.Infrastructure.Converters;
using TrainingTask.Infrastructure.DbOperators;
using TrainingTask.Infrastructure.Models;

namespace TrainingTask.Infrastructure.Repositories
{
    public class ProjectTaskRepository : IProjectTaskRepository<ProjectTask>
    {
        IDbOperator DbOperator;
        IConvertDal<ProjectTask, DataTable> Converter;

        public ProjectTaskRepository()
        {
            DbOperator = new DbOperator();
            Converter = new ProjectTaskDalConverter();
        }

        public void Create(ProjectTask item)
        {
            string SqlQueryString = $"INSERT INTO ProjectTask (Name, TimeToComplete, BeginDate, EndDate, Status, ProjectId, EmployeeId) VALUES (@Name, @TimeToComplete, @BeginDate, @EndDate, @Status, @ProjectId, @EmployeeId)";
            List<SqlParameter> QueryParameters = new List<SqlParameter>
            {
                new SqlParameter("@Name", item.Name),
                new SqlParameter("@TimeToComplete", item.TimeToComplete),
                new SqlParameter("@BeginDate", item.BeginDate),
                new SqlParameter("@EndDate", item.EndDate),
                new SqlParameter("@Status", item.Status),
                new SqlParameter("@ProjectId", item.ProjectId),
                new SqlParameter("@EmployeeId", item.EmployeeId)
            };
            DbOperator.ExecuteNonQuery(SqlQueryString, QueryParameters);
        }

        public void Delete(int id)
        {
            string SqlQueryString = $"DELETE FROM ProjectTask WHERE id = @Id";
            List<SqlParameter> QueryParameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id)
            };
            DbOperator.ExecuteNonQuery(SqlQueryString, QueryParameters);
        }

        public List<ProjectTask> GetAll()
        {
            string SqlQueryString = "SELECT * FROM ProjectTask";
            List<ProjectTask> ProjectTasksList = Converter.ConvertAll(DbOperator.GetData(SqlQueryString));

            return ProjectTasksList;
        }

        public List<ProjectTask> GetAllByProjectId(int id)
        {
            string SqlQueryString = $"SELECT * FROM ProjectTask WHERE ProjectId = @ProjectId";
            List<SqlParameter> QueryParameters = new List<SqlParameter>
            {
                new SqlParameter("@ProjectId", id)
            };

            List<ProjectTask> Projects = ConvertToProjectTasksList(DbOperator.GetData(SqlQueryString, QueryParameters));

            return Projects;
        }

        public ProjectTask GetSingle(int id)
        {
            string SqlQueryString = $"SELECT * FROM ProjectTask WHERE Id = @Id";
            List<SqlParameter> QueryParameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id)
            };
            ProjectTask ProjectTask = Converter.Convert(DbOperator.GetData(SqlQueryString, QueryParameters));

            return ProjectTask;
        }

        public void Update(ProjectTask item)
        {
            string SqlQueryString = $"UPDATE ProjectTask SET Name = @Name, TimeToComplete = @TimeToComplete, BeginDate = @BeginDate, EndDate = @EndDate, Status = @Status, EmployeeId = @EmployeeId, ProjectId = @ProjectId WHERE Id = @Id";
            List<SqlParameter> QueryParameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", item.Id),
                new SqlParameter("@Name", item.Name),
                new SqlParameter("@TimeToComplete", item.TimeToComplete),
                new SqlParameter("@BeginDate", item.BeginDate),
                new SqlParameter("@EndDate", item.EndDate),
                new SqlParameter("@Status", item.Status),
                new SqlParameter("@ProjectId", item.ProjectId),
                new SqlParameter("@EmployeeId", item.EmployeeId)
            };
            DbOperator.ExecuteNonQuery(SqlQueryString, QueryParameters);
        }

        private ProjectTask ConvertToProjectTask(DataTable dataTable)
        {
            return Converter.ConvertAll(dataTable)[0];
        }
        private List<ProjectTask> ConvertToProjectTasksList(DataTable dataTable)
        {
            return Converter.ConvertAll(dataTable);
        }
    }
}
