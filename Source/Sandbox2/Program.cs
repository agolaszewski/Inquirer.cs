using System;
using System.Linq;
using InquirerCS.Beta2.Components;
using InquirerCS.Beta2.Questions;

namespace Sandbox2
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var displayQuestion = new DisplayQuestion<ConsoleColor>();

            displayQuestion.Register(new MessageComponent("Test"));
            displayQuestion.Register(new ConvertToStringComponent<ConsoleColor>());
            displayQuestion.Register(new DefaultValueComponent<ConsoleColor>(ConsoleColor.Blue));
            var question = new QuestionList<ConsoleColor>();

            var choices = new ListChoices<ConsoleColor>(Enum.GetValues(typeof(ConsoleColor)).Cast<ConsoleColor>());

            var choicesComponent = new DisplayChoices<ConsoleColor>();
            choicesComponent.Register(choices);
            choicesComponent.Register(new ConvertToStringComponent<ConsoleColor>());

            question.Register(displayQuestion);
            question.Register(choicesComponent);
            question.Register(choices);
            question.Register(new ReadConsoleKey());
            question.Prompt();
            Console.ReadKey();
        }
    }
}