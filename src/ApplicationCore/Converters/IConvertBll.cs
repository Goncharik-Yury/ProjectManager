using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Converters
{
    interface IConvertBll<TEntity, TDto>
    {
        TEntity Convert(TDto item);
        TDto Convert(TEntity item);
        List<TEntity> ConvertList(List<TDto> items);
        List<TDto> ConvertList(List<TEntity> items);
    }
}
