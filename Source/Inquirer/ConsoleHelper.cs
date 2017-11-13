using System;
using System.Text;

namespace InquirerCS
{
    internal static class ConsoleHelper
    {
        internal static void PositionWrite(string text, int x = 0, int y = 0, ConsoleColor color = ConsoleColor.White)
        {
            Console.SetCursorPosition(x, y);
            Write(text, color);
        }

        internal static void PositionWriteLine(string text, int x = 0, int y = 0, ConsoleColor color = ConsoleColor.White)
        {
            Console.SetCursorPosition(x, y);
            WriteLine(text, color);
        }

        internal static string Read(out bool isCanceled)
        {
            isCanceled = false;

            StringBuilder stringBuilder = new StringBuilder();
            ConsoleKeyInfo keyInfo;
            bool returnToPreviousLine = false;

            do
            {
                keyInfo = Console.ReadKey();
                switch (keyInfo.Key)
                {
                    case (ConsoleKey.Escape):
                        {
                            isCanceled = true;
                            return string.Empty;
                        }

                    case (ConsoleKey.Backspace):
                        {
                            if (stringBuilder.Length > 0)
                            {
                                if (returnToPreviousLine)
                                {
                                    Console.SetCursorPosition(Console.BufferWidth - 1, Console.CursorTop - 1);
                                    returnToPreviousLine = false;
                                }

                                stringBuilder = stringBuilder.Remove(stringBuilder.Length - 1, 1);

                                int oldL = Console.CursorLeft;
                                int oldT = Console.CursorTop;
                                PositionWrite(" ", oldL, oldT);
                                Console.SetCursorPosition(oldL, oldT);

                                if (Console.CursorLeft == 0)
                                {
                                    returnToPreviousLine = true;
                                }
                            }
                            else
                            {
                                Console.SetCursorPosition(Console.CursorLeft + 1, Console.CursorTop);
                            }

                            break;
                        }

                    case (ConsoleKey.Enter):
                        {
                            break;
                        }

                    default:
                        {
                            if (!char.IsControl(keyInfo.KeyChar))
                            {
                                stringBuilder.Append(keyInfo.KeyChar);
                                returnToPreviousLine = false;
                            }
                            else
                            {
                                Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                            }

                            break;
                        }
                }
            }
            while (keyInfo.Key != ConsoleKey.Enter);

            return stringBuilder.ToString();
        }

        internal static ConsoleKey ReadKey(out bool isCanceled)
        {
            isCanceled = false;

            var key = Console.ReadKey().Key;
            if (key == ConsoleKey.Escape)
            {
                isCanceled = true;
            }

            return key;
        }

        internal static void Write(string text, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ResetColor();
        }

        internal static void WriteError(string error)
        {
            WriteLine(string.Empty);
            Write(">> ", ConsoleColor.Red);
            Write(error);
        }

        internal static void WriteLine(string text = " ", ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }
    }
}
