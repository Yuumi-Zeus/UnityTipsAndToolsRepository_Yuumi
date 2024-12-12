using System;

namespace _2_Unity_Tips.Create_Behaviour_Trees.BehaviourTrees
{
    public interface IStrategy
    {
        Node.Status Process();

        void Reset()
        {
            // Noop
        }
    }

    public class ActionStrategy : IStrategy
    {
        readonly Action _doSomething;
        public ActionStrategy(Action doSomething) => _doSomething = doSomething;

        public Node.Status Process()
        {
            _doSomething();
            return Node.Status.Success;  
        }
    }

    public class Condition : IStrategy
    {
        readonly Func<bool> _predicate;
        public Condition(Func<bool> predicate) => _predicate = predicate;

        public Node.Status Process() => _predicate() ? Node.Status.Success : Node.Status.Failure;
    }
}