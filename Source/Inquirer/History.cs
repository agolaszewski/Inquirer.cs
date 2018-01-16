using System.Collections.Generic;

namespace InquirerCS
{
    public static class History
    {
        public static Stack<BaseNode> Stack { get; set; } = new Stack<BaseNode>();
    }
}