using System;
using System.IO;

namespace Cas27.Lib
{
    class Logger
    {
        private static string logFilename = @"C:\SeleniumLogger\test.logger";

        public static void setFileName(string fileName)
        {
            logFilename = fileName;
        }

        public static void empty()
        {
            File.WriteAllText(logFilename, string.Empty);
        }

        public static void log(string messageType, string logMessage)
        {
            writeLog($"[{DateTime.Now}] {messageType}: {logMessage}");
        }

        public static void beginTest(string testName)
        {
            writeLog(separator());
            writeLog($"Staring test: {testName}");
        }

        public static void endTest()
        {
            writeLog(separator());
        }

        private static void writeLog(string logMessage)
        {
            using (StreamWriter fileHandle = new StreamWriter(logFilename, true))
            {
                fileHandle.WriteLine(logMessage);
            }
        }

        private static string separator(char character = '=')
        {
            return new string(character, 100);
        }
    }
}