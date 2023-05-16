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

            Console.Write("Hello, do you have a greeting for your coworkers? (Press enter to just see others' greetings)\n");
            Console.Write(">>> ");
            string newLine = Console.ReadLine();

            if (ContainsShouting(newLine))
            {
                theLogger.LogError("Shout attempt by " + LogAux.ObscureUsername(username) + ": \"" + LogAux.Scrub(newLine) + "\"");
                Console.Write("Please use moderate tones...\n");
                return;
            }

            Console.Write("Here are some greetings from others...\n");
            ShowGreetings(FILENAME);

            if (newLine.Length != 0)
            {
                try
                {
                    File.AppendAllText(FILENAME, newLine + "\n");
                    theLogger.LogInformation("Added: \"" + LogAux.Scrub(newLine) + "\"");
                }
                catch (Exception ex)
                {
                    theLogger.LogWarning("Error writing to greetings file " + FILENAME + ": " + ex.Message);
                }
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
                theLogger.LogWarning("Greetings file " + a_filename + " not found");
            }
        }
    }
}
