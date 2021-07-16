using System;
using System.IO;

namespace TestLogger
{
    class Logger
    {
        private static string LogFilename = @"C:\SeleniumLogger\test.logger";
        private static string InfoMessageType = "INFO";
        private static string ErrorMessageType = "ERROR";

        public static void SetFileName(string fileName)
        {
            LogFilename = fileName;
        }

        public static void Empty()
        {
            File.WriteAllText(LogFilename, string.Empty);
        }

        public static void Info(string logMessage)
        {
            WriteLog($"[{DateTime.Now}] {InfoMessageType}: {logMessage}");
        }

        public static void Error(string logMessage)
        {
            WriteLog($"[{DateTime.Now}] {ErrorMessageType}: {logMessage}");
        }

        public static void BeginTest(string testName)
        {
            WriteLog(Separator());
            WriteLog($"Staring test: {testName}");
        }

        public static void EndTest()
        {
            WriteLog(Separator());
        }

        private static void WriteLog(string logMessage)
        {
            using (StreamWriter fileHandle = new StreamWriter(LogFilename, true))
            {
                fileHandle.WriteLine(logMessage);
            }
        }

        private static string Separator(char character = '=')
        {
            return new string(character, 100);
        }
    }
}