using System;
using System.Collections.Generic;
using System.Data;
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
        //public string GetProjectShortNameById(int id)
        //{
        //    string SqlQueryString = $"SELECT ShortName FROM Project where Id = @Id";

        //    List<SqlParameter> QueryParameters = new List<SqlParameter>
        //    {
        //        new SqlParameter("@Id", id)
        //    };

        //    string ProjectShortName = DBGetData(SqlQueryString, QueryParameters).ToString();
        //    return ProjectShortName;
        //}

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

        protected override object DataParse(DataTable dataTable)
        {
            List<Project> Projects = new List<Project>();
            if (dataTable != null)
            {
                foreach (DataRow dr in dataTable.Rows)
                {
                    Projects.Add(new Project
                    {
                        Id = dr.Field<int>("Id"),
                        Name = dr.Field<string>("Name"),
                        ShortName = dr.Field<string>("ShortName"),
                        Description = dr.Field<string>("Description"),
                    });
                }
            }
            return Projects;
        }
    }

}
