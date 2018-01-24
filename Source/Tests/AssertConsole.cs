using System;
using System.Collections.Generic;
using System.Linq;
using InquirerCS;

namespace Tests
{
    public class AssertConsole : IConsole
    {
        private int _currentY = 0;

        private Dictionary<int, string> buffer = new Dictionary<int, string>();

        public AssertConsole()
        {
        }

        public int CursorLeft
        {
            get { return 0; }
        }

        public int CursorTop
        {
            get { return _currentY; }
        }

        public string ExceptedResult
        {
            get
            {
                return string.Join(Environment.NewLine, buffer.Values.ToList());
            }
        }

        public ConsoleKey IntteruptedKey { get; set; }

        public bool IsCanceled { get; set; }

        public Queue<ConsoleKeyInfo> ReadKeyValue { get; set; } = new Queue<ConsoleKeyInfo>();

        public string ReadValue { get; set; }

        public void Clear()
        {
            buffer.Clear();
        }

        public void PositionWrite(string text, int x = 0, int y = 0, ConsoleColor color = ConsoleColor.White)
        {
            _currentY = y;
            if (!buffer.ContainsKey(y))
            {
                buffer.Add(y, string.Empty);
            }
            buffer[_currentY] = buffer[y].Insert(x, text);
        }

        public void PositionWriteLine(string text, int x = 0, int y = 0, ConsoleColor color = ConsoleColor.White)
        {
            _currentY = y;
            if (!buffer.ContainsKey(y + 1))
            {
                buffer.Add(y + 1, string.Empty);
            }
            buffer[_currentY] = buffer[y + 1].Insert(x, text);
        }

        public string Read()
        {
            return ReadValue;
        }

        public string Read(out ConsoleKey? intteruptedKey, Func<char, bool> allowTypeFn, params ConsoleKey[] interruptKeys)
        {
            intteruptedKey = IntteruptedKey;
            return ReadValue;
        }

        public ConsoleKeyInfo ReadKey()
        {
            return ReadKeyValue.Dequeue();
        }

        public ConsoleKeyInfo ReadKey(out bool isCanceled)
        {
            isCanceled = IsCanceled;
            return ReadKeyValue.Dequeue();
        }

        public void SetCursorPosition(int v, int cursorTop)
        {
        }

        public void Write(string text, ConsoleColor color = ConsoleColor.White)
        {
            if (!buffer.ContainsKey(_currentY))
            {
                buffer.Add(_currentY, string.Empty);
            }

            buffer[_currentY] = buffer[_currentY].Insert(0, text);
        }

        public void WriteError(string error)
        {
            if (!buffer.ContainsKey(_currentY))
            {
                buffer.Add(_currentY, string.Empty);
            }
            buffer[_currentY] = buffer[_currentY].Insert(0, error);
        }

        public void WriteLine(string text = " ", ConsoleColor color = ConsoleColor.White)
        {
            _currentY += 1;

            if (!buffer.ContainsKey(_currentY))
            {
                buffer.Add(_currentY, string.Empty);
            }

            buffer[_currentY] = buffer[_currentY].Insert(0, text);
        }
    }
}