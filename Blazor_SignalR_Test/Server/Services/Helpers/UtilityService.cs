using Blazor_SignalR_Test.Server.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor_SignalR_Test.Server.Services.Helpers
{
    public class UtilityService : IUtilityService
    {
        public bool AskYesNoQuestions(string Question)
        {
            Console.Write(Question += " (Y/N) ");
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                ConsoleKey key = Console.ReadKey().Key;
                Console.ResetColor();
                if (key == ConsoleKey.Y)
                {
                    Console.WriteLine();
                    return true;
                }
                else if (key == ConsoleKey.N)
                {
                    Console.WriteLine();
                    return false;
                }
                else
                {
                    Console.WriteLine("Invalid input!");
                }
            }
        }

        public void PrintText(string text)
        {
            Console.WriteLine(text);
        }

        public void PrintText(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        public void PrintText(string text, bool ExtraWhiteLine)
        {
            Console.WriteLine(text);
            if (ExtraWhiteLine)
            {
                Console.WriteLine();
            }
        }

        public void PrintText(string text, bool ExtraWhiteLine, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            if (ExtraWhiteLine)
            {
                Console.WriteLine();
            }
            Console.ResetColor();
        }
    }
}
