using System.Collections.Generic;
using _2_Unity_Tips.How_To_Serialize_Interfaces.Example;
using _2_Unity_Tips.How_To_Serialize_Interfaces.Example.Scripts;
using _2_Unity_Tips.How_To_Serialize_Interfaces.Odin_Interface_Reference.Scripts;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _2_Unity_Tips.How_To_Serialize_Interfaces.Odin_Interface_Reference.Example.Scripts
{
    public class OdinInterfaceReferenceExample : MonoBehaviour
    {
        [Title("OdinInterfaceReference 自定义类")] public OdinInterfaceReference<IDamageable> scriptableObjectReference;

        public OdinInterfaceReference<IDamageable> componentReference;

        [Title("OdinRequiredInterface Attribute")] [OdinRequiredInterface(typeof(IDamageable))]
        public MonoBehaviour target;

        [OdinRequiredInterface(typeof(IDamageable))]
        public ScriptableObject asset;

        [Title("数组和列表")] public OdinInterfaceReference<IDamageable>[] array;
        public List<OdinInterfaceReference<IDamageable>> list = new List<OdinInterfaceReference<IDamageable>>();

        void Start()
        {
            scriptableObjectReference.InterfaceValue.Damage(10);
            var damage = componentReference.InterfaceValue;
            damage.Damage(20);
        }
    }
}