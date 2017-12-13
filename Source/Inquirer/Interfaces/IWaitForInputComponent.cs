using System;

namespace InquirerCS.Interfaces
{
    public interface IWaitForInputComponent<TInput>
    {
        Func<char, bool> AllowTypeFn { get; set; }

        TInput WaitForInput();
    }
}
