using System;
using InquirerCS;

namespace ConsoleApp1
{
    public abstract class BaseClass
    {
        private static Inquirer _inq;

        public static DoesClass Prompt(string msg)
        {
            return new DoesClass();
        }
    }

    public class DoesClass
    {
        public void Does(Action action)
        {
        }
    }
}