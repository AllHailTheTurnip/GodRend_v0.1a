using System;
using static System.Console;

namespace GodRendSource
{
    public static class Input
    {
        public static string PromptString(string prompt)
        {
            bool isValid = false;
            string input = "";

            while (!isValid)
            {
                // Delivers message to prompt user.
                Message.Narrate(prompt);

                // Get input from user as string.
                input = ReadLine();

                // Check if string is valid; no whitespaces and is not empty.
                if (String.IsNullOrWhiteSpace(input))
                {
                    Message.Narrate("String cannot be empty! Please try again.");

                    // Invalid remains false.
                }
                else
                {
                    isValid = true;
                }
            }

            return input;
        }

        public static void PromptTargetName(out string name)
        {
            name = PromptString("Please choose a target.");
        }
    }
}