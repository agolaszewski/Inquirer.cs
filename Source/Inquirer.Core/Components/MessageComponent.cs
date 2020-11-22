using System;
using System.Collections.Generic;
using System.Text;

namespace Inquirer.Core.Components
{
    public class MessageComponent
    {
        private readonly Func<string> _messageFn;

        public MessageComponent(Func<string> messageFn)
        {
            _messageFn = messageFn;
        }

        public void Draw()
        {
            Console.WriteLine(_messageFn());
        }
    }
}
