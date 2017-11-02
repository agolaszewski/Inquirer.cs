using System;
using System.Collections.Generic;

namespace ConsoleWizard
{
    public class QuestionExtendedList<TDictionary, TResult> : QuestionDictionaryListBase<TDictionary, TResult> where TDictionary : Dictionary<ConsoleKey, TResult>, new()
    {
        internal QuestionExtendedList(string question) : base(question)
        {
        }

        public Func<ConsoleKey, TResult> ParseFn { get; set; } = v => { return default(TResult); };

        public Func<ConsoleKey, bool> ValidatationFn { get; set; } = v => { return true; };

        public QuestionExtendedList<TDictionary, TResult> ConvertToString(Func<TResult, string> fn)
        {
            ConvertToStringFn = fn;
            return this;
        }

        public QuestionExtendedList<TDictionary, TResult> Parse(Func<ConsoleKey, TResult> fn)
        {
            ParseFn = fn;
            return this;
        }

        public QuestionExtendedList<TDictionary, TResult> Validation(Func<ConsoleKey, bool> fn)
        {
            ValidatationFn = fn;
            return this;
        }

        public QuestionExtendedList<TDictionary, TResult> WithConfirmation()
        {
            HasConfirmation = true;
            return this;
        }

        public QuestionExtendedList<TDictionary, TResult> WithDefaultValue(TResult defaultValue)
        {
            DefaultValue = defaultValue;
            HasDefaultValue = true;
            return this;
        }

        internal override TResult Prompt()
        {
            bool tryAgain = true;
            TResult answer = DefaultValue;

            while (tryAgain)
            {
                DisplayQuestion();

                Console.WriteLine();
                Console.WriteLine();

                foreach (var item in Choices)
                {
                    ConsoleHelper.WriteLine(DisplayChoice(item.Key));
                }

                Console.WriteLine();
                ConsoleHelper.Write("Answer: ");

                bool isCanceled = false;
                var key = ConsoleHelper.ReadKey(out isCanceled);
                if (isCanceled)
                {
                    IsCanceled = isCanceled;
                    return default(TResult);
                }

                if (key == ConsoleKey.Enter && HasDefaultValue)
                {
                    tryAgain = Confirm(ConvertToStringFn(answer));
                }
                else if (ValidatationFn(key))
                {
                    answer = ParseFn(key);
                    tryAgain = Confirm(ConvertToStringFn(answer));
                }
            }

            Console.WriteLine();
            return answer;
        }

        private string DisplayChoice(ConsoleKey key)
        {
            return $"[{key}] {Choices[key]}";
        }
    }
}