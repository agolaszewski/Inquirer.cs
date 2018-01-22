using System.Collections.Generic;
using System.Linq;

namespace InquirerCS
{
    public static class History
    {
        public static Dictionary<int, List<BaseNode>> ScopedStack = new Dictionary<int, List<BaseNode>>();

        public static int Scope { get; set; }

        public static BaseNode Pop(bool isParent = false)
        {
            if (Scope == 0)
            {
                return ScopedStack[Scope].Last();
            }

            if (ScopedStack.ContainsKey(Scope) && ScopedStack[Scope].Count > 1)
            {
                int lastIndex = ScopedStack[Scope].Count - 1;
                return ScopedStack[Scope][lastIndex - 1];
            }
            else if (ScopedStack.ContainsKey(Scope) && isParent)
            {
                return ScopedStack[Scope][ScopedStack[Scope].Count - 1];
            }

            --Scope;
            return Pop(true);
        }

        public static void Push(BaseNode node)
        {
            if (ScopedStack.ContainsKey(Scope))
            {
                if (ScopedStack[Scope].All(x => x.Id != node.Id))
                {
                    ScopedStack[Scope].Add(node);
                }
            }
            else
            {
                var list = new List<BaseNode>();
                list.Add(node);
                ScopedStack.Add(Scope, list);
            }
        }

        public static BaseNode Next(BaseNode node)
        {
            if (ScopedStack.ContainsKey(Scope))
            {
                return ScopedStack[Scope].SkipWhile(x => x.Id != node.Id).Skip(1).FirstOrDefault();
            }

            return null;
        }

        internal static void IncreaseScope()
        {
            Scope++;
        }

        internal static void DecreaseScope()
        {
            int nextScope = Scope + 1;
            while (ScopedStack.ContainsKey(nextScope))
            {
                ScopedStack[nextScope].Clear();
                nextScope++;
            }

            Scope--;
        }
    }
}