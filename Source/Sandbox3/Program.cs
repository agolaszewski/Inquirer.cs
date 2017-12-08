using System;
using System.Linq;
using InquirerCS.Builders.NewFolder1;

namespace Sandbox3
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var colors = Enum.GetValues(typeof(ConsoleColor)).Cast<ConsoleColor>().ToList();
            new CheckboxBuilder<ConsoleColor>("Test", colors.Select(x => new InquirerCS.Components.Selectable<ConsoleColor>(false, x)).ToList())
                .WithDefaultValue(ConsoleColor.Red)
                .WithConvertToString(x => x + "Test").Build().Prompt();
        }
    }
}