using System;
using System.Collections.Generic;
using System.Linq;

namespace _2_Unity_Tips.Create_Behaviour_Trees.BehaviourTrees
{
    // UntilSuccess
    // Repeat
    public class UntilFailNode : Node
    {
        public UntilFailNode(string name) : base(name) { }

        public override Status Process()
        {
            if (Children[0].Process() != Status.Failure) return Status.Running;
            Reset();
            return Status.Failure;
        }
    }

    public class InverterNode : Node
    {
        public InverterNode(string name) : base(name) { }

        public override Status Process()
        {
            return Children[0].Process() switch
            {
                Status.Running => Status.Running,
                Status.Success => Status.Failure,
                _ => Status.Success
            };
        }
    }

    public class RandomSelectorNode : PrioritySelectorNode
    {
        protected override List<Node> SortChildren() => Shuffle(Children);
        public RandomSelectorNode(string name) : base(name) { }

        static List<Node> Shuffle(List<Node> list)
        {
            for (var i = list.Count - 1; i > 0; i--)
            {
                var r = UnityEngine.Random.Range(0, i + 1);
                // 通过析构交换
                (list[i], list[r]) = (list[r], list[i]);
            }

            return list;
        }

        public override Status Process()
        {
            var randomChild = Children[UnityEngine.Random.Range(0, Children.Count)];
            switch (randomChild.Process())
            {
                case Status.Running:
                    return Status.Running;
                case Status.Success:
                    return Status.Success;
                default:
                    return Status.Failure;
            }
        }
    }

    public class PrioritySelectorNode : SelectorNode
    {
        public PrioritySelectorNode(string name) : base(name) { }
        public List<Node> SortedChildren => _sortedChildren ??= SortChildren();
        List<Node> _sortedChildren;

        protected virtual List<Node> SortChildren() =>
            Children.OrderByDescending(child => child.Priority)
                .ToList();

        public override void Reset()
        {
            base.Reset();
            _sortedChildren = null;
        }

        public override Status Process()
        {
            foreach (var child in SortedChildren)
            {
                switch (child.Process())
                {
                    case Status.Running:
                        return Status.Running;
                    case Status.Success:
                        return Status.Success;
                    default:
                        continue;
                }
            }

            return Status.Failure;
        }
    }

    public class SelectorNode : Node
    {
        public SelectorNode(string name, int priority = 0) : base(name, priority) { }

        public override Status Process()
        {
            if (CurrentChildIndex < Children.Count)
            {
                switch (Children[CurrentChildIndex].Process())
                {
                    case Status.Running:
                        return Status.Running;
                    case Status.Success:
                        Reset();
                        return Status.Success;
                    default:
                        CurrentChildIndex++;
                        return Status.Running;
                }
            }

            Reset();
            return Status.Failure;
        }
    }

    public class SequenceNode : Node
    {
        public SequenceNode(string name, int priority = 0) : base(name, priority) { }

        public override Status Process()
        {
            if (CurrentChildIndex < Children.Count)
            {
                var status = Children[CurrentChildIndex].Process();
                switch (status)
                {
                    case Status.Running:
                        return Status.Running;
                    case Status.Failure:
                        Reset();
                        return Status.Failure;
                    default:
                        CurrentChildIndex++;
                        return CurrentChildIndex == Children.Count ? Status.Success : Status.Running;
                }
            }

            Reset();
            return Status.Success;
        }
    }

    public class BehaviourTree : Node
    {
        public BehaviourTree(string name) : base(name) { }

        public override Status Process()
        {
            while (CurrentChildIndex < Children.Count)
            {
                var status = Children[CurrentChildIndex].Process();
                if (status != Status.Success)
                {
                    return status;
                }

                CurrentChildIndex++;
            }

            return Status.Success;
        }
    }

    public class LeafNode : Node
    {
        readonly IStrategy _strategy;

        public LeafNode(string nodeName, IStrategy strategy, int priority = 0) : base(nodeName, priority)
        {
            _strategy = strategy;
        }

        public override Status Process() => _strategy.Process();

        public override void Reset() => _strategy.Reset();
    }

    public class Node
    {
        public enum Status
        {
            Running,
            Success,
            Failure
        }

        public readonly string NodeName;
        public readonly int Priority;

        public readonly List<Node> Children = new List<Node>();
        protected int CurrentChildIndex;

        public Node(string nodeName = "Node", int priority = 0)
        {
            NodeName = nodeName;
            Priority = priority;
        }

        public void AddChild(Node child) => Children.Add(child);

        public virtual Status Process() => Children[CurrentChildIndex].Process();

        public virtual void Reset()
        {
            CurrentChildIndex = 0;
            foreach (var child in Children) child.Reset();
        }
    }
}