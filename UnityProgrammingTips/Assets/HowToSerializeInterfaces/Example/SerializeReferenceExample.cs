using System;
using System.Collections.Generic;
using HowToSerializeInterfaces.UnityOriginal;
using Sirenix.OdinInspector;
using UnityEngine;

namespace HowToSerializeInterfaces.Example
{
    public class SerializeReferenceExample : MonoBehaviour
    {
        [RequireInterface(typeof(IDamageable))]
        public MonoBehaviour[] referenceWithAttributeArray;

        public InterfaceReference<IDamageable>[] referenceArray;

        [RequireInterface(typeof(IDamageable))]
        public List<MonoBehaviour> referenceWithAttributeList;

        public List<InterfaceReference<IDamageable>> referenceList;

        public InterfaceReference<IDamageable, ScriptableObject> referenceRestrictedToScriptableObject;
        public InterfaceReference<IDamageable, MonoBehaviour> referenceRestrictedToMonoBehaviour;
        public InterfaceReference<IDamageable> damageableReference;

        [RequireInterface(typeof(IDamageable))]
        public MonoBehaviour attributeRestrictToMono;

        [RequireInterface(typeof(IDamageable))]
        public ScriptableObject attributeRestrictToScriptableObject;

        [SerializeReference] public IDamageable damageable;

        void Start()
        {
            damageableReference.Value.Damage(10);

            IDamageable damage = damageableReference.Value;
            damage.Damage(10);

            if (damageable == null)
            {
                Debug.Log("Damageable is null");
            }
        }
    }
}