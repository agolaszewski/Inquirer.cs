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

    public interface A
    {
        void Derp();
    }

    public interface B
    {
    }

    public class C : A, B
    {
        public void Derp()
        {
            throw new NotImplementedException();
        }
    }

    public static class DerpExtensions
    {
        public static T Test<T>(this T value) where T : B
        {
            return value;
        }

        public static T Test2<T>(this T value) where T : A
        {
            return value;
        }
    }
}