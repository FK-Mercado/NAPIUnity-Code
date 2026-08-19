using UnityEngine;
using NAPI.Data;

namespace NAPI.Combat
{
    public static class DamageScalingUtility
    {
        public static int Calculate(
            DamageScalingType type,
            float value,
            Combatant source,
            Combatant target,
            DamageResult result)
        {
            switch(type)
            {
                case DamageScalingType.Flat:
                    return Mathf.RoundToInt(value);

                case DamageScalingType.CharacterAttack:
                    return Mathf.RoundToInt(
                        source.Attack * value);

                case DamageScalingType.TargetMaxHP:
                    return Mathf.RoundToInt(
                        target.MaxHP * value);

                case DamageScalingType.DamageDealt:
                    return Mathf.RoundToInt(
                        result.Damage * value);

                default:
                    return 0;
            }
        }
    }
}