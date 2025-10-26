using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_6
{
    public static class InputHelper
    {
        public static int GetIntValue(string infoMessage, string errorMessage, int min = int.MinValue, int max = int.MaxValue)
        {
            int value;
            while (true)
            {
                Console.Write(infoMessage);
                string input = Console.ReadLine();

                if (int.TryParse(input, out value) && value >= min && value <= max)
                {
                    return value;
                }
                else
                {
                    Console.WriteLine(errorMessage);
                }
            }
        }

        public static double GetDoubleValue(string infoMessage, string errorMessage, double min = double.MinValue, double max = double.MaxValue)
        {
            double value;
            while (true)
            {
                Console.Write(infoMessage);
                string input = Console.ReadLine();

                if (double.TryParse(input, out value) && value >= min && value <= max)
                {
                    return value;
                }
                else
                {
                    Console.WriteLine(errorMessage);
                }
            }
        }

        public static string GetStringValue(string infoMessage, string errorMessage, Func<string, bool> validator)
        {
            while (true)
            {
                Console.Write(infoMessage);
                string input = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(input) && validator(input))
                {
                    return input;
                }
                else
                {
                    Console.WriteLine(errorMessage);
                }
            }
        }

        public static bool GetYesNo(string infoMessage)
        {
            while (true)
            {
                Console.Write(infoMessage);
                string input = Console.ReadLine()?.ToLower();

                if (input == "y" || input == "yes") return true;
                if (input == "n" || input == "no") return false;

                Console.WriteLine("Please enter Y/N.");
            }
        }
    }
}
