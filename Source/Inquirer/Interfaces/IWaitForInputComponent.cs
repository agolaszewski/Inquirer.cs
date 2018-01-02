using System;
using System.Collections.Generic;

namespace InquirerCS.Interfaces
{
    public interface IWaitForInputComponent<TInput>
    {
        Func<char, bool> AllowTypeFn { get; set; }

        List<ConsoleKey> IntteruptedKeys { get; set; }

        TInput WaitForInput();
    }
}
