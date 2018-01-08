using System;
using System.Collections.Generic;

namespace InquirerCS
{
    public static class History
    {
        public static Stack<Action> Stack { get; set; } = new Stack<Action>();
    }
}