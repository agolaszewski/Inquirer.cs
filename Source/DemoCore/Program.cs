using System;
using Inquirer.Core;
using Inquirer.Core.Interfaces;

namespace DemoCore
{
    class Program
    {
        static void Main(string[] args)
        {
            string derp = string.Empty;
            new Input<int>().Message("asdasd").Prompt();
            Console.WriteLine(derp);
        }
    }
}
