using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using TrainingTask.ApplicationCore.DTO;
using TrainingTask.Infrastructure.Functional;
using TrainingTask.Infrastructure.Models;

namespace TrainingTask.ApplicationCore.DBManipulators
{
    public class ProjectDBManipulator : DBManipulator
    {
        public List<ProjectDTO> GetProjectsList()
        {
            string SqlQueryString = "SELECT * FROM Project";
            var ProjectsList = (List<Project>)DBGetData(SqlQueryString);
            return DTOConverter.ProjectToDTO(ProjectsList);
        }

        public List<ProjectDTO> GetProjectById(int id)
        {
            string SqlQueryString = $"SELECT * FROM Project where Id = @Id";

            List<SqlParameter> QueryParameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id)
            };

            var ProjectsList = (List<Project>)DBGetData(SqlQueryString, QueryParameters);
            return DTOConverter.ProjectToDTO(ProjectsList);
        }

        public bool CreateProject(ProjectDTO project)
        {
            List<SqlParameter> QueryParameters = new List<SqlParameter>
            {
                new SqlParameter("@Name", project.Name),
                new SqlParameter("@ShortName", project.ShortName),
                new SqlParameter("@Description", project.Description)
            };

            string SqlQueryString = $"INSERT INTO Project (Name, ShortName, Description) VALUES (@Name, @ShortName, @Description)";

            DBDoAction(SqlQueryString, QueryParameters);
            return true;
        }

        public bool DeleteProject(int id)
        {
            string SqlQueryString = $"DELETE FROM Project WHERE Id = @Id";
            List<SqlParameter> QueryParameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id)
            };
            DBDoAction(SqlQueryString, QueryParameters);
            return true;
        }

        public bool EditProject(int id, ProjectDTO project)
        {
            string SqlQueryString = $"UPDATE Project SET Name = @Name, ShortName = @ShortName, Description = @Description WHERE Id = @Id";

            List<SqlParameter> QueryParameters = new List<SqlParameter>
            {
                new SqlParameter("@Name", project.Name),
                new SqlParameter("@ShortName", project.ShortName),
                new SqlParameter("@Description", project.Description),
                new SqlParameter("@Id", id)
            };

            DBDoAction(SqlQueryString, QueryParameters);
            return true;
        }

        protected override object DataParse(SqlDataReader dataReader)
        {
            List<Project> projectsList = new List<Project>();
            while (dataReader.Read())
            {
                projectsList.Add(new Project
                {
                    Id = (int)dataReader.GetValue(0),
                    Name = (string)dataReader.GetValue(1),
                    ShortName = (string)dataReader.GetValue(2),
                    Description = (string)dataReader.GetValue(3)
                });
            }
            return projectsList;
        }
    }

}
