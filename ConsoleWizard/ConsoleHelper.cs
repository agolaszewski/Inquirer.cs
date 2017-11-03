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

        public static void WriteError(string error)
        {
            WriteLine(string.Empty);
            Write(">> ", ConsoleColor.Red);
            Write(error);
        }

        public static void WriteLine(string text = " ", ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        public static void PositionWrite(string text, int x = 0, int y = 0, ConsoleColor color = ConsoleColor.White)
        {
            Console.SetCursorPosition(x, y);
            Write(text, color);
        }

        public static void PositionWriteLine(string text, int x = 0, int y = 0, ConsoleColor color = ConsoleColor.White)
        {
            Console.SetCursorPosition(x, y);
            WriteLine(text, color);
        }

        public static string Read(out bool isCanceled)
        {
            isCanceled = false;

            string result = string.Empty;
            ConsoleKeyInfo keyInfo;
            var moveLine = false;

            do
            {
                keyInfo = Console.ReadKey();
                switch (keyInfo.Key)
                {
                    case (ConsoleKey.Escape):
                        {
                            isCanceled = true;
                            return result;
                        }

                    case (ConsoleKey.Backspace):
                        {
                            if (result.Length > 0)
                            {
                                if (moveLine)
                                {
                                    Console.SetCursorPosition(Console.BufferWidth - 1, Console.CursorTop - 1);
                                    moveLine = false;
                                }

                                result = result.Remove(result.Length - 1, 1);

                                int oldL = Console.CursorLeft;
                                int oldT = Console.CursorTop;
                                PositionWrite(" ", oldL, oldT);
                                Console.SetCursorPosition(oldL, oldT);

                                if (Console.CursorLeft == 0)
                                {
                                    moveLine = true;
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
                            if (keyInfo.Key >= ConsoleKey.A && keyInfo.Key <= ConsoleKey.Z)
                            {
                                result += keyInfo.KeyChar;
                                moveLine = false;
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

            return result;
        }

        public static ConsoleKey ReadKey(out bool isCanceled)
        {
            isCanceled = false;

            var key = Console.ReadKey().Key;
            if (key == ConsoleKey.Escape)
            {
                isCanceled = true;
            }

            return key;
        }
    }
}