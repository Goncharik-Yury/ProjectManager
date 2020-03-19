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
    public class ProjectTaskDBManipulator : DBManipulator
    {
        EmployeeDBManipulator EmployeeDBManipulator = new EmployeeDBManipulator();
        public List<ProjectTaskDTO> GetAllProjectTasksList()
        {
            string SqlQueryString = "SELECT * FROM ProjectTask";
            var ProjectTasksList = (List<ProjectTask>)DBGetData(SqlQueryString);

            return GetProjectTaskDTOList(SqlQueryString);
        }
        public List<ProjectTaskDTO> GetProjectTasksById(int id)
        {
            string SqlQueryString = $"SELECT * FROM ProjectTask WHERE Id = @Id";
            List<SqlParameter> QueryParameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id)
            };

            return GetProjectTaskDTOList(SqlQueryString, QueryParameters);
        }
        public List<ProjectTaskDTO> GetProjectTasksByProjectId(int projectId)
        {
            string SqlQueryString = $"SELECT * FROM ProjectTask WHERE ProjectId = @ProjectId";
            List<SqlParameter> QueryParameters = new List<SqlParameter>
            {
                new SqlParameter("@ProjectId", projectId)
            };
            

            return GetProjectTaskDTOList(SqlQueryString, QueryParameters);
        }

        private List<ProjectTaskDTO> GetProjectTaskDTOList(string SqlQueryString, List<SqlParameter> QueryParameters = null)
        {
            var ProjectTasksList = (List<ProjectTask>)DBGetData(SqlQueryString, QueryParameters);
            var ProjectTasksListDTO = DTOConverter.ProjectTaskToDTO(ProjectTasksList);
            ProjectTasksListDTO.ForEach(pt =>
            {
                var EmployeesVM = EmployeeDBManipulator.GetEmployeeById(pt.Id);
                if (EmployeesVM.Count > 0)
                {
                    pt.EmloyeeFullName = $"{EmployeesVM[0].LastName} {EmployeesVM[0].FirstName} {EmployeesVM[0].Patronymic}";
                }
            });
            return ProjectTasksListDTO;
        }

        public static bool CreateProjectTask(ProjectTaskDTO projectTask)
        {
            string SqlQueryString = $"INSERT INTO ProjectTask (Name, TimeToComplete, BeginDate, EndDate, Status, ProjectId) VALUES (@Name, @TimeToComplete, @BeginDate, @EndDate, @Status, @ProjectId)";
            List<SqlParameter> QueryParameters = new List<SqlParameter>
            {
                new SqlParameter("@Name", projectTask.Name),
                new SqlParameter("@TimeToComplete", projectTask.TimeToComplete),
                new SqlParameter("@BeginDate", projectTask.BeginDate),
                new SqlParameter("@EndDate", projectTask.EndDate),
                new SqlParameter("@Status", projectTask.Status),
                new SqlParameter("@ProjectId", projectTask.ProjectId)
            };
            DBDoAction(SqlQueryString, QueryParameters);
            return true;
        }

        public static bool DeleteProjectTask(int id)
        {
            string SqlQueryString = $"DELETE FROM ProjectTask WHERE id = @Id";
            List<SqlParameter> QueryParameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id)
            };
            DBDoAction(SqlQueryString, QueryParameters);
            return true;
        }

        public static bool EditProjectTask(int id, ProjectTaskDTO projectTask)
        {
            string SqlQueryString = $"UPDATE ProjectTask SET Name = @Name, TimeToComplete = @TimeToComplete, BeginDate = @BeginDate, EndDate = @EndDate, Status = @Status WHERE Id = @Id";
            List<SqlParameter> QueryParameters = new List<SqlParameter>
            {
                new SqlParameter("@Name", projectTask.Name),
                new SqlParameter("@TimeToComplete", projectTask.TimeToComplete),
                new SqlParameter("@BeginDate", projectTask.BeginDate),
                new SqlParameter("@EndDate", projectTask.EndDate),
                new SqlParameter("@Status", projectTask.Status),
                new SqlParameter("@Id", id)
            };
            DBDoAction(SqlQueryString, QueryParameters);
            return true;
        }

        protected override object DataParse(DataTable dataTable)
        {
            List<ProjectTask> ProjectTasks = new List<ProjectTask>();
            foreach (DataRow dr in dataTable.Rows)
            {
                ProjectTasks.Add(new ProjectTask
                {
                    Id = dr.Field<int>("Id"),
                    Name = dr.Field<string>("Name"),
                    TimeToComplete = dr.Field<int>("TimeToComplete"),
                    BeginDate = dr.Field<DateTime?>("BeginDate"),
                    EndDate = dr.Field<DateTime?>("EndDate"),
                    Status = dr.Field<string>("Status"),
                    ProjectId = dr.Field<int>("ProjectId"),
                    EmployeeId = dr.Field<int?>("EmployeeId"),
                });
            }
            return ProjectTasks;
        }
    }

}
