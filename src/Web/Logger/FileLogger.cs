using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace TrainingTask.Web.Logger
{
    public class FileLogger : ILogger
    {
        private static readonly string FilePath;

        static FileLogger()
        {
            string DirectoryPath = $"../../Logs/";
            FilePath = DirectoryPath + $"{DateTime.Now.ToString("yyyy.MM.dd")}.log";
            DirectoryInfo LogsDirecory = new DirectoryInfo(DirectoryPath);
            if (!LogsDirecory.Exists)
            {
                LogsDirecory.Create();
            }
        }
        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            string TextToLog = $"{logLevel} | {DateTime.Now} | {formatter(state, exception)}{Environment.NewLine}";

            File.AppendAllText(FilePath, TextToLog);
        }
    }
}
