using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManager.ApplicationCore.Repository
{
    public interface IProjectTaskService<TDto> : IService<TDto> where TDto : class
    {
        IList<TDto> GetAllByProjectId(int id);
    }
}
