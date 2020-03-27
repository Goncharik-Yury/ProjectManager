using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Common.Logger
{
    public class FileLogger : ILogger
    {
        private readonly string filePath;
        private readonly string directoryPath;

        public FileLogger(/*string directoryPath*/)
        {
            //directoryPath = directoryPath;
            directoryPath = $"../../Logs/";
            filePath = directoryPath + $"{DateTime.Now.ToString("yyyy.MM.dd")}.log";
            DirectoryInfo LogsDirecory = new DirectoryInfo(directoryPath);
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

            File.AppendAllText(filePath, TextToLog);
        }
    }
}
