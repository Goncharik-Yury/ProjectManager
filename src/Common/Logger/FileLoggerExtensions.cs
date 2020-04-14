using Microsoft.Extensions.Logging;

namespace Common.Logger
{
    public static class FileLoggerExtensions
    {
        public static void LogError(this ILogger logger, string message)
        {
            logger.Log(LogLevel.Error, message);
        }

        public static void LogDebug(this ILogger logger, string message)
        {
            logger.Log(LogLevel.Debug, message);
        }

        public static void LogTrace(this ILogger logger, string message)
        {
            logger.Log(LogLevel.Trace, message);
        }
    }
}
