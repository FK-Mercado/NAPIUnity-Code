using UnityEngine;
using NAPI.Data;

namespace NAPI.Combat
{
    public abstract class StatusEffect
    {
        public string EffectName { get; protected set; }
        public int RemainingTurns { get; protected set; }

        protected StatusEffect(int duration)
        {
            RemainingTurns = duration;
        }

        public virtual void OnApply(Combatant target) { }
        public virtual void OnTurnStart(Combatant target) { }

        public virtual void OnTurnEnd(Combatant target)
        {
            RemainingTurns--;
            Debug.Log($"{EffectName} restantes: {RemainingTurns}");
        }

        public virtual void OnRemove(Combatant target) { }

        public bool IsExpired => RemainingTurns <= 0;

        /// <summary>
        /// Categoría a fines de limpieza masiva. Default: ninguna de las
        /// dos (así quedan afuera los DOT sin que tengan que declarar
        /// nada). Cada clase de Buff/ marca IsBuff, cada una de Debuff/
        /// marca IsDebuff.
        /// </summary>
        public virtual bool IsBuff => false;
        public virtual bool IsDebuff => false;

        public virtual bool ShouldSkipHolderTurn(Combatant holder) => false;
        public virtual void OnDamageDealtByHolder(Combatant holder, Combatant target, SkillData skill, int damageDealt, bool wasCrit) { }
        public virtual int ModifyOutgoingDamage(Combatant holder, Combatant target, SkillData skill, int outgoingDamage) => outgoingDamage;
        public virtual void OnHolderUsedSkill(Combatant holder, SkillData skill) { }
        public virtual int ModifyIncomingDamage(Combatant holder, Combatant attacker, SkillData incomingSkill, int incomingDamage) => incomingDamage;
    }
}
