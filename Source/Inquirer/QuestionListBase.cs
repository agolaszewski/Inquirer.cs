using System;
using System.Collections.Generic;
using System.Linq;

namespace InquirerCS
{
    public abstract class QuestionListBase<TResult> : QuestionSingleChoiceBase<int?, TResult>
    {
        internal QuestionListBase(string message) : base(message)
        {
        }

        protected QuestionListBase(QuestionListBase<TResult> questionListBase) : base(questionListBase.Message)
        {
            Choices = questionListBase.Choices;
            ParseFn = questionListBase.ParseFn;
            ValidatorsTInput = questionListBase.ValidatorsTInput;
        }

        internal List<TResult> Choices { get; set; }

        public abstract QuestionListBase<TResult> Page(int pageSize);

        public override QuestionSingleChoiceBase<int?, TResult> WithDefaultValue(TResult defaultValue, Func<TResult, TResult, int> compareFn = null)
        {
            if ((typeof(TResult) is IComparable || typeof(TResult).IsEnum || typeof(TResult).IsValueType) && compareFn == null)
            {
                compareFn = (x, y) =>
                {
                    var x1 = x as IComparable;
                    var y1 = y as IComparable;
                    return x1.CompareTo(y1);
                };
            }
            else if (compareFn == null)
            {
                throw new Exception("compareFn not defined");
            }

            if (Choices.Where(x => compareFn(x, defaultValue) == 0).Any())
            {
                var index = Choices.Select((answer, i) => new { Value = answer, Index = i }).First(x => compareFn(x.Value, defaultValue) == 0).Index;
                Choices.Insert(0, Choices[index]);
                Choices.RemoveAt(index + 1);

                DefaultValue = Choices[0];
                HasDefaultValue = true;
            }
            else
            {
                throw new Exception("No default values in choices");
            }

            return this;
        }
    }
}
