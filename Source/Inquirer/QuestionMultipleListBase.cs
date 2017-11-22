using System;
using System.Collections.Generic;
using System.Linq;

namespace InquirerCS
{
    public abstract class QuestionMultipleListBase<TList, TResult> : QuestionBase<TList> where TList : List<TResult>, new()
    {
        private TList _choices;

        public QuestionMultipleListBase(QuestionCheckbox<TList, TResult> questionCheckbox) : base(questionCheckbox.Message)
        {
            Choices = questionCheckbox.Choices;
        }

        internal QuestionMultipleListBase(string message) : base(message)
        {
        }

        internal TList Choices
        {
            get
            {
                return _choices;
            }

            set
            {
                _choices = value;
                Selected = new bool[value.Count];
            }
        }

        internal Func<TResult, string> ConvertToStringFn { get; set; } = value => { return value.ToString(); };

        internal Func<ConsoleKey> ReadFn { get; set; }

        internal bool[] Selected { get; private set; }

        protected List<Tuple<Func<TList, bool>, Func<TList, string>>> ValidatorsTResults { get; set; } = new List<Tuple<Func<TList, bool>, Func<TList, string>>>();

        public QuestionMultipleListBase<TList, TResult> ConvertToString(Func<TResult, string> fn)
        {
            ConvertToStringFn = fn;
            return this;
        }

        public QuestionMultipleListBase<TList, TResult> WithConfirmation()
        {
            HasConfirmation = true;
            return this;
        }

        public QuestionMultipleListBase<TList, TResult> WithDefaultValue(TList defaultValue, Func<TResult, TResult, int> compareFn = null)
        {
            if ((typeof(TResult) is IComparable || typeof(TResult).IsEnum || typeof(TResult).IsValueType) && compareFn == null)
            {
                compareFn = (l, r) =>
                {
                    var l1 = l as IComparable;
                    var r1 = r as IComparable;
                    return l1.CompareTo(r1);
                };
            }
            else if (compareFn == null)
            {
                throw new Exception("compareFn not defined");
            }

            DefaultValue = defaultValue;
            foreach (var value in defaultValue)
            {
                if (Choices.Where(item => compareFn(item, value) == 0).Any())
                {
                    var index = Choices.Select((answer, i) => new { Value = answer, Index = i }).First(x => compareFn(x.Value, value) == 0).Index;
                    Selected[index] = true;
                }
                else
                {
                    throw new Exception("No default values in choices");
                }
            }

            HasDefaultValue = true;
            return this;
        }

        public QuestionMultipleListBase<TList, TResult> WithDefaultValue(TResult defaultValue, Func<TResult, TResult, int> compareFn = null)
        {
            if ((typeof(TResult) is IComparable || typeof(TResult).IsEnum || typeof(TResult).IsValueType) && compareFn == null)
            {
                compareFn = (l, r) =>
                {
                    var l1 = l as IComparable;
                    var r1 = r as IComparable;
                    return l1.CompareTo(r1);
                };
            }
            else if (compareFn == null)
            {
                throw new Exception("compareFn not defined");
            }

            DefaultValue = new TList { defaultValue };
            if (Choices.Where(item => compareFn(item, defaultValue) == 0).Any())
            {
                var index = Choices.Select((answer, i) => new { Value = answer, Index = i }).First(x => compareFn(x.Value, defaultValue) == 0).Index;
                Selected[index] = true;
            }
            else
            {
                throw new Exception("No default values in choices");
            }

            HasDefaultValue = true;
            return this;
        }

        public QuestionMultipleListBase<TList, TResult> WithValidation(Func<TList, bool> fn, Func<TList, string> errorMessageFn)
        {
            ValidatorsTResults.Add(new Tuple<Func<TList, bool>, Func<TList, string>>(fn, errorMessageFn));
            return this;
        }

        public QuestionMultipleListBase<TList, TResult> WithValidation(Func<TList, bool> fn, string errorMessage)
        {
            ValidatorsTResults.Add(new Tuple<Func<TList, bool>, Func<TList, string>>(fn, answers => { return errorMessage; }));
            return this;
        }

        protected void DisplayQuestion()
        {
            Console.Clear();
            ConsoleHelper.Write("[?] ", ConsoleColor.Yellow);
            var question = $"{Message} : ";
            if (HasDefaultValue)
            {
                question += $"[{string.Join(",", DefaultValue.Select(x => ConvertToStringFn(x)))}] ";
            }

            ConsoleHelper.Write(question);
        }

        protected bool Validate(TList answer)
        {
            foreach (var validator in ValidatorsTResults)
            {
                if (!validator.Item1(answer))
                {
                    ConsoleHelper.WriteError(validator.Item2(answer));
                    return false;
                }
            }

            return true;
        }
    }
}
