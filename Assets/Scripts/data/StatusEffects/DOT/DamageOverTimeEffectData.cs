using UnityEngine;
using NAPI.Combat;

namespace NAPI.Data
{
    /// <summary>
    /// Molde compartido por todos los efectos de daño por turno (DOT).
    /// Concentra acá el escalado (Flat / ataque del atacante / vida
    /// máxima del objetivo / daño ya infligido) y la creación del
    /// runtime genérico. Una variación concreta (Quemadura, Veneno,
    /// Escarcha...) normalmente NO necesita sobreescribir nada de esto:
    /// solo hereda, agrega su [CreateAssetMenu] propio, y listo aparece
    /// como una entrada más en el menú — igual que pasa con los Buff.
    /// </summary>
    public abstract class DamageOverTimeEffectData : StatusEffectData
    {
        [Header("Escalado del daño por turno")]
        public DamageScalingType scalingType;
        public float scalingValue = 10f;

        public override StatusEffect CreateEffect(
            Combatant source,
            Combatant target,
            SkillData skill,
            DamageResult damageResult)
        {
            int damagePerTurn =
                DamageScalingUtility.Calculate(
                    scalingType,
                    scalingValue,
                    source,
                    target,
                    damageResult);

            // effectName viene de StatusEffectData: así cada asset (ej.
            // "Quemadura de Markus" vs "Quemadura de Jack") puede mostrar
            // un nombre distinto en UI aunque compartan la misma clase.
            return new DamageOverTimeEffect(duration, damagePerTurn, effectName);
        }
    }
}
