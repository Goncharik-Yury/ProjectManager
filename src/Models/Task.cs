using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Training_task.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TimeToComplete { get; set; } // ??? Формат
        public DateTime BeginDate { get; set; } // ??? В каком формате даты?
        public DateTime EndDate { get; set; } // ??? Формат
        public TaskStatus Status { get; set; } // ??? Какой тип должен быть? Можно enum?
    }
}
