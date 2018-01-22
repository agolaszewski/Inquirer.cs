using System.Collections.Generic;
using System.Linq;

namespace InquirerCS
{
    public static class History
    {
        public static Dictionary<int, List<BaseNode>> ScopedStack = new Dictionary<int, List<BaseNode>>();

        public static int Scope { get; set; }

        public static BaseNode Next(BaseNode node)
        {
            if (ScopedStack.ContainsKey(Scope))
            {
                return ScopedStack[Scope].SkipWhile(x => x.Id != node.Id).Skip(1).FirstOrDefault();
            }

            return null;
        }

        public static BaseNode Pop(BaseNode node)
        {
            int lastIndex = ScopedStack[Scope].FindIndex(x => x.Id == node.Id) - 1;
            if (lastIndex >= 0)
            {
                return ScopedStack[Scope][lastIndex];
            }

            if (Scope - 1 == 0)
            {
                return ScopedStack[1].FirstOrDefault();
            }

            return ScopedStack[Scope - 1].Last();
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

        internal static void IncreaseScope()
        {
            Scope++;
        }

        private static BaseNode Pop()
        {
            if (Scope < 1)
            {
                Scope = 1;
                return ScopedStack[Scope].FirstOrDefault();
            }

            if (ScopedStack.ContainsKey(Scope))
            {
                int lastIndex = ScopedStack[Scope].Count - 1;
                return ScopedStack[Scope][lastIndex];
            }

            --Scope;
            return Pop();
        }
    }
}