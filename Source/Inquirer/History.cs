namespace InquirerCS
{
    internal static class History
    {
        private static BaseNode _root;

        public static int Scope { get; set; }

        public static BaseNode Next(BaseNode node)
        {
            return node.Next;
        }

        public static BaseNode Pop(BaseNode node)
        {
            if (node.Previous != null)
            {
                return node.Previous;
            }

            if (node.Parent != null)
            {
                Scope--;
                var parent = node.Parent;
                parent.Child = null;
                parent.IsCurrent = true;
                return parent;
            }

            return _root;
        }

        public static void Push(BaseNode node)
        {
            node.ScopeLevel = Scope;

            if (_root == null)
            {
                _root = node;
                _root.IsCurrent = true;
                return;
            }

            var currentNode = GetCurrent(_root, Scope);

            if (currentNode.ScopeLevel == Scope)
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

            return GetCurrent(node.Next ?? node.Child, scope);
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