using Sirenix.OdinInspector;
using UnityEngine;

namespace _2_Unity_Tips.Create_Behaviour_Trees.BehaviourTrees.Example.Scripts
{
    public class BehaviourTreeLaunchBasic : MonoBehaviour
    {
        public bool canDo;
        readonly BehaviourTree _tree = new BehaviourTree("Root");

        [Button("Basic")]
        void Basic()
        {
            _tree.AddChild(new LeafNode("GO", new ActionStrategy(() => Debug.Log("Do Action"))));
            _tree.AddChild(new LeafNode("Condition", new Condition(() => canDo)));

            var status = _tree.Process();
            Debug.Log(status);
        }
    }
}