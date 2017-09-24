using System;

namespace ConsoleWizard
{
    public static class ConsoleHelper
    {
        public static void Write(string text, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ResetColor();
        }

        public static void WriteLine(string text, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        public static void WriteError(string error)
        {
            Write(">> ", ConsoleColor.Red);
            Write(error);
        }
    }
}