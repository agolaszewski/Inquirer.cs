using System;

namespace InquirerCS
{
    public interface IConsole
    {
        void PositionWrite(string text, int x = 0, int y = 0, ConsoleColor color = ConsoleColor.White);

        void PositionWriteLine(string text, int x = 0, int y = 0, ConsoleColor color = ConsoleColor.White);

        string Read();

        string Read(out ConsoleKey? intteruptedKey, params ConsoleKey[] interruptKeys);

        ConsoleKeyInfo ReadKey();

        ConsoleKeyInfo ReadKey(out bool isCanceled);

        void Write(string text, ConsoleColor color = ConsoleColor.White);

        void Clear();

        void WriteError(string error);

        void WriteLine(string text = " ", ConsoleColor color = ConsoleColor.White);
    }
}