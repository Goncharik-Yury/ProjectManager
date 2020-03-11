using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrainingTask.WEB.Services
{
    public class Test01Service : ITest
    {
        public string MyMethod()
        {
            return $"Service from {this.GetType()}";
        }
    }

    public class Test02Service : ITest
    {
        public string MyMethod()
        {
            return $"Service from {this.GetType()}";
        }
    }
}
