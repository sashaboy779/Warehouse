using System;
using System.Collections.Generic;

namespace PL.Helpers
{
    public static class ConsoleDisplay
    {
        public static void Collection<T>(List<T> collecton, string collectionName)
        {
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine($"{Environment.NewLine}{collectionName}{Environment.NewLine}");
            Console.ResetColor();

            for (int i = 0; i < collecton.Count; i++)
            {
                Console.WriteLine(collecton[i]);
            }
        }

        public static void Message(string message, ConsoleColor color)
        {
            Console.BackgroundColor = color;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}
