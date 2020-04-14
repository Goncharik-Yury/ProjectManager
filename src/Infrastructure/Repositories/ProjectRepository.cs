using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using ProjectManager.Infrastructure.Models;
using System.Linq;
using Microsoft.Extensions.Logging;
using ProjectManager.Infrastructure.Converters;

namespace ProjectManager.Infrastructure.Repositories
{
    public class ProjectRepository : BaseRepository<Project>, IRepository<Project>
    {
        protected override string ConnectionString { get; }
        private readonly Func<SqlDataReader, IList<Project>> projectConverterDelegate;

        public ProjectRepository(string connectionString, IConvertDb<SqlDataReader, Project> projectConverter, ILogger logger) : base(logger)
        {
            ConnectionString = connectionString;
            projectConverterDelegate = projectConverter.Convert;
        }

        static List<Project> ConvertToProject(SqlDataReader sqlDataReader)
        {
            List<Project> Projects = new List<Project>();
            while (sqlDataReader.Read())
            {
                Project project = new Project();

                project.Id = sqlDataReader.GetInt32("Id");
                project.Name = sqlDataReader.GetString("Name");
                project.ShortName = sqlDataReader.GetString("ShortName");
                project.Description = sqlDataReader["Description"] as string;

                Projects.Add(project);
            }
            return Projects;
        }

        public void Create(Project item)
        {
            logger.LogDebug(GetType() + ".Create is called");
            string SqlQueryString = $"INSERT INTO Project (Name, ShortName, Description) VALUES (@Name, @ShortName, @Description)";
            ExecuteNonQuery(SqlQueryString, GetCreateParameters(item));
        }

        public void Delete(int id)
        {
            logger.LogDebug(GetType() + ".Delete is called");
            string SqlQueryString = $"DELETE FROM Project WHERE Id = @Id";
            ExecuteNonQuery(SqlQueryString, GetIdParameter(id));
        }

        public IList<Project> GetAll()
        {
            logger.LogDebug(GetType() + ".GetAll is called");
            string SqlQueryString = "SELECT * FROM Project";
            return GetData(SqlQueryString, projectConverterDelegate);
        }

        public Project Get(int id)
        {
            logger.LogDebug(GetType() + ".Get is called");
            string SqlQueryString = $"SELECT * FROM Project where Id = @Id";
            Project Project = GetData(SqlQueryString, projectConverterDelegate, GetIdParameter(id)).FirstOrDefault();
            return Project;
        }

        public void Update(Project item)
        {
            logger.LogDebug(GetType() + ".Update is called");
            string SqlQueryString = $"UPDATE Project SET Name = @Name, ShortName = @ShortName, Description = @Description WHERE Id = @Id";
            ExecuteNonQuery(SqlQueryString, GetUpdateParameters(item));
        }

        private List<SqlParameter> GetCreateParameters(Project item)
        {
            return new List<SqlParameter>
            {
                new SqlParameter("@Name", item.Name),
                new SqlParameter("@ShortName", item.ShortName),
                new SqlParameter("@Description", SqlDbType.NVarChar){Value = item.Description ?? (object)DBNull.Value}
            };
        }

        private List<SqlParameter> GetUpdateParameters(Project item)
        {
            List<SqlParameter> SqlParameters = GetCreateParameters(item);
            SqlParameters.AddRange(GetIdParameter(item.Id));
            return SqlParameters;
        }

        private List<SqlParameter> GetIdParameter(int id)
        {
            List<SqlParameter> QueryParameters = new List<SqlParameter>();
            QueryParameters.Add(new SqlParameter("@Id", id));
            return QueryParameters;
        }
    }
}
