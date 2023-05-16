using System;
using System.IO;

namespace SharpGreetings
{
    class Greetings
    {
        public static void Main(string[] args)
        {
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
