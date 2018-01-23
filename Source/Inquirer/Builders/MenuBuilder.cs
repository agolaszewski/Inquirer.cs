using System;
using System.Collections.Generic;
using InquirerCS.Components;
using InquirerCS.Interfaces;
using InquirerCS.Questions;
using InquirerCS.Traits;

namespace InquirerCS.Builders
{
    public class MenuBuilder : InputBuilder<ConsoleList<MenuAction>, int, MenuAction>, IRenderChoicesTrait<MenuAction>
    {
        public MenuBuilder(string header, IConsole console) : base(console)
        {
            Choices = new List<MenuAction>();

            this.RenderQuestion(header, this, this, console);
            this.Parse(Choices);
            this.RenderChoices(Choices, this, Console);
            this.Parse(Choices);
            this.Input(Console, true, ConsoleKey.Enter, ConsoleKey.DownArrow, ConsoleKey.UpArrow);

            this.ConvertToString(item => item.Description);
        }

        public List<MenuAction> Choices { get; set; }

        public IRenderChoices<MenuAction> RenderChoices { get; set; }

        public override ConsoleList<MenuAction> Build()
        {
            return new ConsoleList<MenuAction>(Choices, Confirm, RenderQuestion, Input, Parse, RenderChoices, ResultValidators, DisplayError, OnKey);
        }

        public MenuBuilder AddOption(string description, Action option)
        {
            Choices.Add(new MenuAction(description, option));
            return this;
        }
    }
}