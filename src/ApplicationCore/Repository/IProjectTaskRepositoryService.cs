using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Repository
{
    public interface IProjectTaskRepositoryService<TDto> : IRepositoryService<TDto> where TDto : class
    {
        List<TDto> GetAllByProjectId(int id);
    }
}
