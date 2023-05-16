// Required nuget packages:
// Microsoft.Extensions.Logging
// Microsoft.Extensions.Logging.Debug

using Microsoft.Extensions.Logging;

using System;
using System.IO;

namespace SharpGreetings
{
    class Greetings
    {
        static ILoggerFactory s_loggerFactory = LoggerFactory.Create(builder =>
        {
            builder
                .AddFilter("Microsoft", LogLevel.Warning)
                .AddFilter("System", LogLevel.Warning)
                .AddFilter("SharpGreetings.Greetings", LogLevel.Debug)
                .AddDebug();
        });

        private static ILogger<T> CreateLogger<T>()
        {
            ILogger<T> logger = s_loggerFactory.CreateLogger<T>();
            return logger;
        }

        static ILogger<Greetings> theLogger = CreateLogger<Greetings>();

        public static void Main(string[] args)
        {
            string username = Environment.UserDomainName + '\\' + Environment.UserName;

            theLogger.LogDebug("This will be an DEBUGGING log entry");
            theLogger.LogInformation("This will be an INFORMATIONAL log entry");
            theLogger.LogWarning("This will be an WARNING log entry");
            theLogger.LogError("This will be an ERROR log entry");
            theLogger.LogCritical("This will be an CRITICAL log entry");

            Console.Write("Hello, do you have a greeting for your coworkers? (Press enter to just see others' greetings)\n");
            Console.Write(">>> ");
            string newLine = Console.ReadLine();

            if (ContainsShouting(newLine))
            {
                Console.Write("Please use moderate tones...\n");
                return;
            }

            Console.Write("Here are some greetings from others...\n");
            ShowGreetings(FILENAME);

            if (newLine.Length != 0)
            {
                File.AppendAllText(FILENAME, newLine + "\n");
            }
        }

        private static bool ContainsShouting(string a_greeting)
        {
            uint contigousUpperCase = 0;
            foreach (char c in a_greeting)
            {
                contigousUpperCase = char.IsUpper(c) ? contigousUpperCase + 1 : 0;
                if (3 == contigousUpperCase)
                {
                    return true;
                }
            }

            return false;
        }

        private static readonly string FILENAME = "greetings.txt";

        private static void ShowGreetings(string a_filename)
        {
            try
            {
                string[] lines = File.ReadAllLines(a_filename);
                foreach (string line in lines)
                {
                    Console.Write("* ");
                    Console.WriteLine(line);
                }
            }
            catch (FileNotFoundException)
            {
                // ignore
            }
        }
    }
}
