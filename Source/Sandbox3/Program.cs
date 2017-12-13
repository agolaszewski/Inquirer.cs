using System;
using System.Linq;
using InquirerCS.Builders;
using InquirerCS.Components;

namespace Sandbox3
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var colors = Enum.GetValues(typeof(ConsoleKey)).Cast<ConsoleKey>().ToList();
            //new CheckboxBuilder<ConsoleColor>("Test", colors.Select(x => new InquirerCS.Components.Selectable<ConsoleColor>(false, x)).ToList())
            //    .WithDefaultValue(ConsoleColor.Red)
            //    .WithConvertToString(x => x + "Test").Build().Prompt();
            //new PagedListBuilder<ConsoleKey>("test",colors.ToList(),5).WithConfirmation().WithDefaultValue(ConsoleKey.B).Build().Prompt();
        }
    }
}