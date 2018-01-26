namespace InquirerCS
{
    internal static class History
    {
        private static BaseNode _root;

        public static int ScopeLevel { get; set; }

        public static BaseNode Next(BaseNode node)
        {
            return node.Next;
        }

        public static BaseNode Pop(BaseNode node)
        {
            node.IsCurrent = false;

            if (node.Previous != null)
            {
                node.Previous.IsCurrent = true;
                return node.Previous;
            }

            if (node.Parent != null)
            {
                var parent = node.Parent;
                History.ScopeLevel = parent.ScopeLevel;
                parent.Child = null;
                parent.IsCurrent = true;
                return parent;
            }

            _root.IsCurrent = true;
            return _root;
        }

        public static void Push(BaseNode node)
        {
            node.ScopeLevel = ScopeLevel;

            if (_root == null)
            {
                _root = node;
                _root.IsCurrent = true;
                return;
            }

            var currentNode = GetCurrent(_root, ScopeLevel);

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

            currentNode.IsCurrent = false;
            node.IsCurrent = true;
        }

        private static BaseNode GetCurrent(BaseNode node, int scope)
        {
            if (node.IsCurrent)
            {
                return GetLocalRoot(node, scope);
            }

            return GetCurrent(node.Child ?? node.Next, scope);
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