using System;
using System.Collections.Generic;
using System.Data;
using TrainingTask.Infrastructure.Models;

namespace TrainingTask.Infrastructure.Converters
{
    class ProjectTaskDalConverter : IConvertDal<ProjectTask, DataTable>
    {
        public ProjectTask Convert(DataTable item)
        {
            ProjectTask ProjectTask = GetRowItem(item.Rows[0]);
            item.Dispose();

            return ProjectTask;
        }

        public IList<ProjectTask> ConvertAll(DataTable items)
        {
            List<ProjectTask> employees = new List<ProjectTask>();
            foreach (DataRow DataRow in items.Rows)
            {
                employees.Add(GetRowItem(DataRow));
            }
            items.Dispose();

            return employees;
        }

        private ProjectTask GetRowItem(DataRow DataRow)
        {
            ProjectTask ProjectTask = new ProjectTask
            {
                Id = DataRow.Field<int>("Id"),
                Name = DataRow.Field<string>("Name"),
                TimeToComplete = DataRow.Field<int>("TimeToComplete"),
                BeginDate = DataRow.Field<DateTime?>("BeginDate"),
                EndDate = DataRow.Field<DateTime?>("EndDate"),
                Status = DataRow.Field<string>("Status"),
                ProjectId = DataRow.Field<int>("ProjectId"),
                EmployeeId = DataRow.Field<int?>("EmployeeId"),
            };

            return ProjectTask;
        }
    }
}
