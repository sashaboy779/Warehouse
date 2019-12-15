using System;
using PL.bin.Debug.Resources;

namespace PL.Helpers
{
    public static class UserValue
    {
        public static string Get(string messageForUser, int attempts, Func<string, bool> checker)
        {
            do
            {
                ConsoleDisplay.Message($"{Environment.NewLine}{messageForUser} ({Messages.Attempts} - {attempts--}): ", ConsoleColor.DarkGray);
                string inputValue = Console.ReadLine();

                if (checker.Invoke(inputValue))
                {
                    return inputValue;
                }
                ConsoleDisplay.Message(WarningMessages.InvalidInput, ConsoleColor.DarkRed);

            } while (attempts > 0);
            throw new Exception($"{WarningMessages.ExceptionAttempts}");
        }
    }
}
