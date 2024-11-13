using System.Collections.Generic;
using HowToSerializeInterfaces.Example;
using HowToSerializeInterfaces.OdinInterfaceReferencePractice.Scripts;
using Sirenix.OdinInspector;
using UnityEngine;

namespace HowToSerializeInterfaces.OdinInterfaceReferencePractice.Example.Scripts
{
    public class OdinInterfaceReferenceExample : MonoBehaviour
    {
        [ShowDrawerChain] public OdinInterfaceReference<IDamageable> reference;

        [ShowDrawerChain]
        public List<OdinInterfaceReference<IDamageable>> references = new List<OdinInterfaceReference<IDamageable>>();
    }
}