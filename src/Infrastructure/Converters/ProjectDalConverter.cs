using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using TrainingTask.Infrastructure.Models;

namespace TrainingTask.Infrastructure.Converters
{
    class ProjectDalConverter : IConvertDal<Project, DataTable>
    {
        public Project Convert(DataTable item)
        {
            throw new NotImplementedException();
        }

        public List<Project> ConvertAll(DataTable items)
        {
            List<Project> Projects = new List<Project>();
            int q = items.Rows.Count;
            foreach (DataRow dr in items.Rows)
            {
                Projects.Add(new Project
                {
                    Id = dr.Field<int>("Id"),
                    Name = dr.Field<string>("Name"),
                    ShortName = dr.Field<string>("ShortName"),
                    Description = dr.Field<string>("Description"),
                });
            }
            items.Dispose();

            return Projects;
        }
    }
}
