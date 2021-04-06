using System;
using System.Linq;
using System.Text;

namespace InquirerCS
{
    internal class AppConsole : IConsole
    {
        public int CursorLeft
        {
            get
            {
                return Console.CursorLeft;
            }
        }

        public int CursorTop
        {
            get
            {
                return Console.CursorTop;
            }
        }

        public void Clear()
        {
            Console.Clear();
        }

        public void PositionWrite(string text, int x = 0, int y = 0, ConsoleColor color = ConsoleColor.White)
        {
            Console.SetCursorPosition(x, y);
            Write(text, color);
        }

        public void PositionWrite(string v, object cursorLeft, int cursorTop)
        {
            throw new NotImplementedException();
        }

        public void PositionWriteLine(string text, int x = 0, int y = 0, ConsoleColor color = ConsoleColor.White)
        {
            Console.SetCursorPosition(x, y);
            WriteLine(text, color);
        }

        public string Read()
        {
            ConsoleKey? interruptKey;
            return Read(out interruptKey, value => { return true; });
        }

        public string Read(out ConsoleKey? intteruptedKey, Func<char, bool> allowTypeFn, params ConsoleKey[] interruptKeys)
        {
            intteruptedKey = null;

            StringBuilder stringBuilder = new StringBuilder();
            ConsoleKeyInfo keyInfo;
            bool returnToPreviousLine = false;

            do
            {
                keyInfo = Console.ReadKey();

                if (interruptKeys.Contains(keyInfo.Key))
                {
                    intteruptedKey = keyInfo.Key;
                    return stringBuilder.ToString();
                }

                switch (keyInfo.Key)
                {
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
                            if (allowTypeFn(keyInfo.KeyChar))
                            {
                                stringBuilder.Append(keyInfo.KeyChar);
                                returnToPreviousLine = false;
                            }
                            else
                            {
                                PositionWrite(" ", Console.CursorLeft - 1, Console.CursorTop);
                                Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                            }

                            break;
                        }
                }
            }
            while (keyInfo.Key != ConsoleKey.Enter);

            return stringBuilder.ToString();
        }

        public ConsoleKeyInfo ReadKey()
        {
            return Console.ReadKey();
        }

        public ConsoleKeyInfo ReadKey(out bool isCanceled)
        {
            isCanceled = false;

            var key = Console.ReadKey();
            if (key.Key == ConsoleKey.Escape)
            {
                isCanceled = true;
            }

            return key;
        }

        public void SetCursorPosition(int x, int y)
        {
            Console.SetCursorPosition(x, y);
        }

        public void Write(string text, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ResetColor();
        }

        public void WriteError(string error)
        {
            Write(">> ", ConsoleColor.Red);
            Write(error);
            WriteLine(string.Empty);
            Write("Press any key to continue");
        }

        public void WriteLine(string text = " ", ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }
    }
}
