using UnityEngine;

namespace _2_Unity_Tips.How_To_Serialize_Interfaces.Example.Scripts
{
    public class DamageableComponent : MonoBehaviour, IDamageable
    {
        public void Damage(int damage)
        {
            Debug.Log($"DamageableComponent took damage: {damage}");
        }
    }
}