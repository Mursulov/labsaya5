using System;
using System.IO;

namespace Lab5App
{
    public static class Logger
    {
        private static readonly object sync = new object();
        private static string? logPath;

        public static void Init()
        {
            try
            {
                var dir = Path.Combine(Directory.GetCurrentDirectory(), "logs");
                Directory.CreateDirectory(dir);
                logPath = Path.Combine(dir, "app.log");
                Log("Logger initialized.");
            }
            catch
            {
                // Не мешаем выполнению приложения, если лог не создаётся
            }
        }

        public static void Log(string message)
        {
            try
            {
                if (string.IsNullOrEmpty(logPath)) return;
                lock (sync)
                {
                    var line = $"{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss} UTC | {message}";
                    File.AppendAllText(logPath, line + Environment.NewLine);
                }
            }
            catch
            {
                // swallow
            }
        }
    }
}
