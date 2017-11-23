using System;

namespace InquirerCS.Beta
{
    public class ValidateResult : IValidationResultComponent
    {
        private string _errorMessage;

        public ValidateResult(string errorMessage)
        {
            _errorMessage = errorMessage;
        }

        public void Run()
        {
            Console.WriteLine();
            ConsoleHelper.WriteError(_errorMessage);
            Console.ReadKey();
        }
    }
}
