using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using TrainingTask.Infrastructure.Repositories;
using TrainingTask.Infrastructure.Models;
using System.Linq;
using TrainingTask.Common;

namespace TrainingTask.Infrastructure.Repositories
{
    public class ProjectRepository : BaseRepository<Project>, IRepository<Project>
    {
        protected override string ConnectionString { get; }

        Func<SqlDataReader, List<Project>> converter = ConvertToProject;
        public ProjectRepository(string connectionString)
        {
            ConnectionString = connectionString;
        }
        static List<Project> ConvertToProject(SqlDataReader sqlDataReader)
        {
            List<Project> Projects = new List<Project>();
            while (sqlDataReader.Read())
            {
                Project project = new Project
                {
                    Id = sqlDataReader.GetInt32("Id"),
                    Name = sqlDataReader.GetString("Name"),
                    ShortName = sqlDataReader.GetString("ShortName"),
                    Description = sqlDataReader.GetString("Description"),
                };

                Projects.Add(project);
            }
            return Projects;
        }

        public void Create(Project item)
        {
            string SqlQueryString = $"INSERT INTO Project (Name, ShortName, Description) VALUES (@Name, @ShortName, @Description)";
            List<SqlParameter> QueryParameters = GetEntityParameters(item);

            ExecuteNonQuery(SqlQueryString, QueryParameters);
        }

        public void Delete(int id)
        {
            string SqlQueryString = $"DELETE FROM Project WHERE Id = @Id";
            List<SqlParameter> QueryParameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id)
            };

            ExecuteNonQuery(SqlQueryString, QueryParameters);
        }

        public IList<Project> GetAll()
        {
            string SqlQueryString = "SELECT * FROM Project";
            return GetData(SqlQueryString, converter, null);
        }

        public Project Get(int id)
        {
            string SqlQueryString = $"SELECT * FROM Project where Id = @Id";

            List<SqlParameter> QueryParameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id)
            };

            Project Project = GetData(SqlQueryString, converter, QueryParameters).FirstOrDefault();
            return Project;
        }

        public void Update(Project item)
        {
            string SqlQueryString = $"UPDATE Project SET Name = @Name, ShortName = @ShortName, Description = @Description WHERE Id = @Id";

            List<SqlParameter> QueryParameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", item.Id)
            };
            QueryParameters.AddRange(GetEntityParameters(item));

            ExecuteNonQuery(SqlQueryString, QueryParameters);
        }

        private List<SqlParameter> GetEntityParameters(Project item)
        {
            return new List<SqlParameter>
            {
                new SqlParameter("@Name", item.Name),
                new SqlParameter("@ShortName", item.ShortName),
                new SqlParameter("@Description", item.Description)
            };
        }
    }
}
