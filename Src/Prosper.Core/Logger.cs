using System;

namespace Prosper.Core
{
    public static class Logger
    {
        public static void Log(SeverityLevel severityLevel, string message)
        {
            SetColor(severityLevel);
            Console.WriteLine($"{severityLevel} - {message}");
        }

        public static void Log(SeverityLevel severityLevel, Exception exception)
        {
            SetColor(severityLevel);
            Console.WriteLine($"{severityLevel} - {exception.Message}");
            Console.WriteLine($"{exception.StackTrace}");
        }

        public static void Log(SeverityLevel severityLevel, string message, Exception exception)
        {
            SetColor(severityLevel);
            Console.WriteLine($"{severityLevel} - {message} - {exception.Message}");
            Console.WriteLine($"{exception.StackTrace}");
        }

        private static void SetColor(SeverityLevel severityLevel)
        {
            switch (severityLevel)
            {
                case SeverityLevel.Success:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case SeverityLevel.Warning:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case SeverityLevel.Error:
                case SeverityLevel.Fatal:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case SeverityLevel.Debug:
                case SeverityLevel.Info:
                default:
                    Console.ResetColor();
                    break;
            }
        }
    }
}
