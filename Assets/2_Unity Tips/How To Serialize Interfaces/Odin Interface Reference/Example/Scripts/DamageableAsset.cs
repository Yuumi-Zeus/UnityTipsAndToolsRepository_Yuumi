using UnityEngine;

namespace _2_Unity_Tips.How_To_Serialize_Interfaces.Example.Scripts
{
    [CreateAssetMenu(fileName = "DamageableAsset", menuName = "SerializeInterface/Example/IDamageable", order = 0)]
    public class DamageableAsset : ScriptableObject, IDamageable
    {
        public void Damage(int damage)
        {
            Debug.Log($"DamageableAsset took damage: {damage}");
        }
    }
}