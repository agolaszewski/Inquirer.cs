using System;

namespace InquirerCS
{
    public class Node
    {
        public Node(Node parent)
        {
            Parent = parent;
            Condition = () => { return true; };
        }

        public Node(Node parent, Node sibling)
        {
            Parent = parent;
            Sibling = sibling;
            Condition = () => { return true; };
        }

        public static Node CurrentNode { get; private set; }

        public Func<bool> Condition { get; private set; }

        public Guid Id { get; private set; } = Guid.NewGuid();

        public Node Next { get; set; }

        public Node Parent { get; private set; }

        public Node Sibling { get; set; }

        public Action Task { get; private set; }

        public void Flow()
        {
            if (Condition())
            {
                Task();
            }

            if (Next != null)
            {
                Flow(Next);
            }
        }

        public void Go()
        {
            CurrentNode = GoToRoot(this);
            CurrentNode.Task();
        }

        public Node Then(Action task)
        {
            if (Task == null)
            {
                Task = () => { CurrentNode = this; task(); };
                return this;
            }

            var node = new Node(this);
            Next = node;

            node.Then(task);
            return node;
        }

        public Node When(Func<bool> condition)
        {
            var node = new Node(this);
            node.Condition = condition;
            Next = node;
            return node;
        }

        private void Flow(Node node)
        {
            if (node.Condition())
            {
                node.Task();
            }

            if (node.Next != null)
            {
                Flow(node.Next);
            }
        }

        private Node GoToRoot(Node node)
        {
            if (node.Parent != null)
            {
                return GoToRoot(node.Parent);
            }

            return node;
        }
    }
}
