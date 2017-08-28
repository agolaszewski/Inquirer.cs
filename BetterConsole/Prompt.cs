using System;
using System.Collections.Generic;
using System.Linq;

namespace BetterConsole
{
    public class Prompt<T>
    {
        internal string _message;
        internal T _default;
        internal Func<string, bool> _validatorFn = v => { return true; };
        internal List<T> _choices = new List<T>();
        internal bool _withConfirmation;
        internal Func<T, string> _choiceToStringFn = v => { return v.ToString(); };
        internal Func<string, T> _parseFn = v => { return v.To<T>(); };
        internal bool _hasDefaultValue;
        internal Func<string> _readModeFn = () => { return Console.ReadLine(); };

        public string RawValue { get; private set; }
        public string ParsedValue { get; private set; }

        public Prompt<T> WithMessage(string message)
        {
            _message = message;
            return this;
        }
        public Prompt<T> WithConfirmation()
        {
            _withConfirmation = true;
            return this;
        }

        public Prompt<T> WithDefault(T @default)
        {
            _hasDefaultValue = true;
            _default = @default;
            return this;
        }

        public Prompt<T> WithValidator(Func<string, bool> validatorFn)
        {
            _validatorFn = validatorFn;
            return this;
        }

        public T SingleChoice(List<T> choices, Func<T, string> choiceToStringFn = null)
        {
            _choices = choices;
            _choiceToStringFn = choiceToStringFn ?? _choiceToStringFn;

            _validatorFn = v =>
            {
                var value = v.ToN<int>();
                if (value.HasValue)
                {
                    if (value > 0 && value <= _choices.Count)
                    {
                        return true;
                    }
                }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Choose value from 1 to {_choices.Count}");
                Console.ResetColor();
                return false;
            };

            _parseFn = v =>
            {
                var index = v.To<int>();
                return choices[index - 1];
            };

            return Run();
        }

        internal T Run()
        {
            bool tryAgain = true;
            T result = _default;

            while (tryAgain)
            {
                DisplayChoices();
                DisplayQuestion();

                RawValue = _readModeFn();

                if ((string.IsNullOrEmpty(RawValue) && _hasDefaultValue) || _validatorFn(RawValue))
                {
                    result = ParseReadLine(RawValue);
                    tryAgain = Confirm(result);
                }
            }
            Console.WriteLine();
            return result;
        }

        private bool Confirm(T result)
        {
            if (_withConfirmation)
            {
                Console.WriteLine($"Are you sure? [y/n] : {result.ToString()} ");
                ConsoleKeyInfo key = default(ConsoleKeyInfo);
                do
                {
                    key = Console.ReadKey();
                }
                while (key.Key != ConsoleKey.Y && key.Key != ConsoleKey.N);

                if (key.Key == ConsoleKey.N)
                {
                    Console.WriteLine();
                    return true;
                }
            }

            return false;
        }

        private T ParseReadLine(string value)
        {
            if (string.IsNullOrEmpty(value) == false)
            {
                return _parseFn(value);
            }
            else
            {
                return _default;
            }
        }

        private void DisplayQuestion()
        {
            if (string.IsNullOrWhiteSpace(_message) == false)
            {
                var question = $"[?] {_message} : ";
                if (_hasDefaultValue)
                {
                    question += $"[{_default}] ";
                }

                Console.Write(question);
            }
        }

        private void DisplayChoices()
        {
            Console.WriteLine();
            for (int i = 0; i < _choices.Count; i++)
            {
                Console.WriteLine($"[{i + 1}] {_choiceToStringFn(_choices[i])}");
            }
        }
    }
}