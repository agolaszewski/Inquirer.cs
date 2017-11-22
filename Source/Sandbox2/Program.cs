using InquirerCS.Beta;

namespace Sandbox2
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var component = new UIInput<int>(new MessageComponent("Test"), new ConvertToStringComponent<int>(), null);
            var validation = new ValidateComponent<string>();
            var parse = new ParseComponent<string, string>(value => { return value; });
            var confirm = new ConfirmComponent<string>();

            var q = new QuestionInput<string>(component, new ReadComponent(), validation, validation, parse, confirm);
            q.Prompt();
        }
    }
}
