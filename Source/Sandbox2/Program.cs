using InquirerCS.Beta2.Components;
using InquirerCS.Beta2.Questions;

namespace Sandbox2
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var displayQuestion = new DisplayQuestion<int>(new MessageComponent("Test"), new ConvertToStringComponent<int>(), new DefaultValueComponent<int>(4));
            new QuestionInput<int>(displayQuestion).Prompt();
        }
    }
}