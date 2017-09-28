using System;
using System.Linq;

namespace ConsoleWizard
{
    public static class QuestionExtensions
    {
        public static QuestionBase<T> WithConfirmation<T>(this QuestionBase<T> question)
        {
            question.HasConfirmation = true;
            return question;
        }

        public static QuestionBase<T> WithDefaultValue<T>(this QuestionBase<T> question, T defaultValue)
        {
            question.DefaultValue = defaultValue;
            question.HasDefaultValue = true;
            return question;
        }

        public static QuestionBase<T> WithDefaultValue<T>(this QuestionRawList<T> question, T defaultValue) where T : IComparable
        {
            var query = question.Choices.Where(x => x.CompareTo(defaultValue) == 0);

            if (query.Any())
            {
                question.DefaultValue = query.First();
                question.HasDefaultValue = true;
            }
            else
            {
                question.Choices.Insert(0,defaultValue);
                question.DefaultValue = defaultValue;
                question.HasDefaultValue = true;
            }
            return question;
        }

        public static QuestionBase<T> WithDefaultValue<T>(this QuestionRawList<T> question, int index) 
        {
            question.DefaultValue = question.Choices[index];
            question.HasDefaultValue = true;
            return question;
        }
    }
}