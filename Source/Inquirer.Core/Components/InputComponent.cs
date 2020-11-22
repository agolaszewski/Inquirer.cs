using System;
using System.Collections.Generic;
using System.Text;

namespace Inquirer.Core.Components
{
    public class InputComponent
    {
        public InputComponent()
        {

        }

        public char Wait()
        {
            return Console.ReadKey().KeyChar;
        }
    }
}
