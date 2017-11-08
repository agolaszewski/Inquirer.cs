using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    internal class Answers
    {
        public string Input { get; internal set; }
        public int InputNumber { get; internal set; }
        public ConsoleKey One { get; set; }
        public ConsoleColor Two { get; internal set; }
        public List<ConsoleColor> Colors { get; internal set; }
    }
}