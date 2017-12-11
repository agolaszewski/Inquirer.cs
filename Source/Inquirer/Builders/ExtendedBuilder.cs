using System;
using System.Linq;
using InquirerCS.Questions;
using InquirerCS.Traits;

namespace InquirerCS.Builders
{
    public class ExtendedBuilder : InputBuilder<InputKey<ConsoleKey>, ConsoleKey, ConsoleKey>
    {
        public ExtendedBuilder(string message, params ConsoleKey[] @params)
        {
            this.RenderQuestion(message, this, this);

            this.Parse(value => { return value; });

            InputValidators.Add(
            value =>
            {
                return @params.Any(p => p == value);
            },
            value =>
            {
                string keys = " Press : ";
                foreach (var key in @params)
                {
                    keys += $"[{(char)key}] ";
                }

                return keys;
            });
        }

        public override InputKey<ConsoleKey> Build()
        {
            return new InputKey<ConsoleKey>(Confirm, RenderQuestion, Input, Parse, ResultValidators, InputValidators, DisplayError, Default, OnKey);
        }
    }
}
