using UnityEngine;

namespace HowToSerializeInterfaces.Example
{
    public class DamageableComponent : MonoBehaviour, IDamageable
    {
        public void Damage(int damage)
        {
            Debug.Log($"DamageableComponent took damage: {damage}");
        }
    }
}