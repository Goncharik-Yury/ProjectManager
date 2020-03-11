using System.Collections.Generic;

namespace TrainingTask
{
    public static class TaskStatus
    {
        public static Dictionary<string, string> statuses = new Dictionary<string, string>() {
            {"NotStarted", "NotStarted"},
            {"InProgress", "InProgress" },
            {"Completed", "Completed"},
            {"Delayed", "Delayed"},
        };
        //static string[] statuses = new string[] { "NotStarted", "InProgress", "Completed", "Delayed" };

    }
    //public enum TaskStatus
    //{
    //    NotStarted,
    //    InProgress,
    //    Completed,
    //    Delayed
    //}

    
}