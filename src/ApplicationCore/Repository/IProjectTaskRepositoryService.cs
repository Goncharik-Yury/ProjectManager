using System;
using System.Collections.Generic;
using System.Text;

namespace TrainingTask.ApplicationCore.Repository
{
    public interface IProjectTaskRepositoryService<TDto> : IRepositoryService<TDto> where TDto : class
    {
        IList<TDto> GetAllByProjectId(int id);
    }
}
