using System;
using System.Collections.Generic;
using System.Text;

namespace TrainingTask.Web.Converters
{
    public interface IConvertWeb<TViewModel, TDto>
    {
        TViewModel Convert(TDto item);
        TDto Convert(TViewModel item);
        List<TViewModel> ConvertAll(List<TDto> items);
        List<TDto> ConvertList(List<TViewModel> items);
    }
}
