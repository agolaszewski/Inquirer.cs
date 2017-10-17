using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleWizard.Components;

namespace ConsoleWizard
{
    public static class QuestionExtensions
    {
        public static T ConvertTypeToString<T, TType>(this T question, Func<TType, string> convertToStringFn) where T : IConvertToString<TType>
        {
            question.ConvertToStringFn = convertToStringFn;
            return question;
        }

        public static QuestionList<T> Paging<T>(this QuestionList<T> question, int pageSize)
        {
            var pagedQuestion = new QuestionPagedList<T>(question);
            pagedQuestion.PageSize = pageSize;
            return pagedQuestion;
        }

        public static QuestionRawList<T> Paging<T>(this QuestionRawList<T> question, int pageSize)
        {
            var pagedQuestion = new QuestionPagedRawList<T>(question);
            pagedQuestion.PageSize = pageSize;
            return pagedQuestion;
        }

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

        public static QuestionCheckbox<List<T>, T> WithDefaultValue<T>(this QuestionCheckbox<List<T>, T> question, List<T> defaultValues) where T : IComparable
        {
            question.DefaultValue = defaultValues;
            foreach (var value in defaultValues)
            {
                if (question.Choices.Where(x => x.CompareTo(value) == 0).Any())
                {
                    var index = question.Choices.Select((v, i) => new { Value = v, Index = i }).First(x => x.Value.CompareTo(value) == 0).Index;
                    question.Selected[index] = true;
                }
                else
                {
                    throw new Exception("No default values in choices");
                }
            }

            question.HasDefaultValue = true;
            return question;
        }

        public static QuestionCheckbox<List<T>, T> WithDefaultValue<T>(this QuestionCheckbox<List<T>, T> question, T defaultValue) where T : IComparable
        {
            question.DefaultValue = new List<T> { defaultValue };
            if (question.Choices.Where(x => x.CompareTo(defaultValue) == 0).Any())
            {
                var index = question.Choices.Select((v, i) => new { Value = v, Index = i }).First(x => x.Value.CompareTo(defaultValue) == 0).Index;
                question.Selected[index] = true;
            }
            else
            {
                throw new Exception("No default values in choices");
            }

            question.HasDefaultValue = true;
            return question;
        }

        public static QuestionBase<T> WithDefaultValue<T>(this QuestionListBase<T> question, T defaultValue) where T : IComparable
        {
            if (question.Choices.Where(x => x.CompareTo(defaultValue) == 0).Any())
            {
                var index = question.Choices.Select((v, i) => new { Value = v, Index = i }).First(x => x.Value.CompareTo(defaultValue) == 0).Index;
                question.Choices.Insert(0, question.Choices[index]);
                question.Choices.RemoveAt(index + 1);

                question.DefaultValue = question.Choices[0];
                question.HasDefaultValue = true;
            }
            else
            {
                throw new Exception("No default values in choices");
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