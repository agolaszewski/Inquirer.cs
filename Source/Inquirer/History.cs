namespace InquirerCS
{
    internal static class History
    {
        private static BaseNode _root;

        public static BaseNode CurrentNode { get; set; }

        public static int ScopeLevel { get; set; }

        public static BaseNode Next(BaseNode node)
        {
            if (!node.IsDone)
            {
                return node.Next;
            }

            return null;
        }

        public static BaseNode Pop(BaseNode node)
        {
            if (node.Previous != null)
            {
                CurrentNode = node.Previous;
                node.Previous.IsDone = false;
                return node.Previous;
            }

            if (node.Parent != null)
            {
                CurrentNode = node.Parent;
                var parent = node.Parent;
                ScopeLevel = parent.ScopeLevel;
                parent.Child = null;
                return parent;
            }

            CurrentNode = _root;
            return _root;
        }

        public static void Push(BaseNode node)
        {
            node.ScopeLevel = ScopeLevel;

            if (_root == null)
            {
                _root = node;
                CurrentNode = _root;
                return;
            }

            var currentNode = GetLocalRoot(CurrentNode, ScopeLevel);

            if (currentNode.ScopeLevel == node.ScopeLevel)
            {
                currentNode.Next = node;
                node.Previous = currentNode;
            }
            else
            {
                currentNode.Child = node;
                node.Parent = currentNode;
            }

            CurrentNode = node;
        }

        private static BaseNode GetLocalRoot(BaseNode node, int scope)
        {
            if (node.ScopeLevel <= scope)
            {
                return node;
            }

            return GetLocalRoot(node.Previous ?? node.Parent, scope);
        }
    }
}