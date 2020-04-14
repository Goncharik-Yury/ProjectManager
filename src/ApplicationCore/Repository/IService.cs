using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManager.ApplicationCore.Repository
{
    public interface IService<TDto> where TDto : class
    {
        IList<TDto> GetAll();
        TDto Get(int id);
        void Create(TDto item);
        void Update(TDto item);
        void Delete(int id);
    }
}
