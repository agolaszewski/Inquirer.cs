using BetterConsole;
using System;
using System.Collections.Generic;

namespace Demo
{
    public class Class1
    {
        private Dictionary<string, InquireBase> _table = new Dictionary<string, InquireBase>();

        public Class1()
        {
            AddQuestion("1", new Inquire("sdas").Confirm()).Navigate(x =>
            {
                if (x)
                {
                    new Inquire("2").Input().WithDefault("MyNewResourceGroup").WithConfirmation().HasAnswer(xx => { Answers.SampleStr = xx; });
                }
                else
                {
                    AddQuestion("1.2", (new Inquire("3").Input().WithDefault("MyNewResourceGroup").WithConfirmation()).Navigate(Question("3"))).Prompt();
                }
            });

            AddQuestion("3", (new Inquire("3").Input().WithDefault("MyNewResourceGroup").WithConfirmation()).Navigate(Question("1.1"))).HasAnswer(x => { Answers.SampleStr = x; });
            Prompt("1");
        }

        private InquireBase<T> AddQuestion<T>(string id, InquireBase<T> inquireBase)
        {
            _table.Add(id, inquireBase);
            return inquireBase;
        }

        public void Prompt(string id)
        {
            _table[id].Prompt();
        }

        public Action Question(string id)
        {
            return () => { _table[id].Prompt(); };
        }
    }
}