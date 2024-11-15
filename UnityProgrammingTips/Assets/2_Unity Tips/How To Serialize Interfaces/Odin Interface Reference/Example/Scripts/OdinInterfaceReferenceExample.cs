using System;
using System.Collections.Generic;
using HowToSerializeInterfaces.Example;
using HowToSerializeInterfaces.OdinInterfaceReferencePractice.Scripts;
using Sirenix.OdinInspector;
using UnityEngine;

namespace HowToSerializeInterfaces.OdinInterfaceReferencePractice.Example.Scripts
{
    public class OdinInterfaceReferenceExample : MonoBehaviour
    {
        // [ShowDrawerChain] 
        public OdinInterfaceReference<IDamageable> scriptableObjectReference;

        public OdinInterfaceReference<IDamageable> componentReference;

        // [ShowDrawerChain]
        public List<OdinInterfaceReference<IDamageable>> references = new List<OdinInterfaceReference<IDamageable>>();

        void Start()
        {
            scriptableObjectReference.InterfaceValue.Damage(10);
            var damage = componentReference.InterfaceValue;
            damage.Damage(20);
        }
    }
}