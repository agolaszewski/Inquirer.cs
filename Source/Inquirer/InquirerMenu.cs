using System;
using System.Collections.Generic;

namespace InquirerCS
{
    public class InquirerMenu
    {
        private string _header;

        private Inquirer _inquirer;

        private List<Tuple<string, Action>> _options = new List<Tuple<string, Action>>();

        internal InquirerMenu(string header, Inquirer inquirer)
        {
            _header = header;
            _inquirer = inquirer;
        }

        public InquirerMenu AddOption(string description, Action option)
        {
            _options.Add(new Tuple<string, Action>(description, option));
            return this;
        }

        public void Prompt()
        {
        }

        private string DisplayChoice(int index)
        {
            return $"{_options[index].Item1}";
        }
    }
}
