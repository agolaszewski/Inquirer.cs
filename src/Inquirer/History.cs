using System.Collections.Generic;

namespace InquirerCS
{
    public static class History
    {
        private static int _current = 0;

        public static NavigationList<BaseNode> CurrentScope
        {
            get
            {
                if (Scopes.ContainsKey(_current))
                {
                    return Scopes[_current];
                }

                Scopes.Add(_current, new NavigationList<BaseNode>());
                return Scopes[_current];
            }
        }

        public static Dictionary<int, NavigationList<BaseNode>> Scopes { get; set; } = new Dictionary<int, NavigationList<BaseNode>>();

        public static void Pop()
        {
            if (_current > 0)
            {
                Scopes.Remove(_current);
                _current -= 1;
            }
        }

        public static void Process(BaseNode node)
        {
            if (node != null)
            {
                var result = node.Run();

                if (result)
                {
                    Process(CurrentScope.Next);
                }
                else
                {
                    var previous = CurrentScope.Previous;
                    if (previous != null)
                    {
                        Process(previous);
                    }
                    else
                    {
                        Pop();
                        Process(CurrentScope.Current);
                    }
                }
            }
        }

        public static void Push()
        {
            _current += 1;
        }
    }
}
