using NAPI.Data;

namespace NAPI.Combat
{
    /// <summary>
    /// Definiciones de los eventos de combate publicados a través de
    /// CombatEventBus. Son datos puros: solamente describen un hecho ya
    /// ocurrido (por eso son readonly struct), no ejecutan nada, no
    /// calculan nada y no contienen reglas de ningún sistema (Ultimate,
    /// CAR, elementos, turnos). Nada publica todavía estos eventos:
    /// esa conexión se hace en etapas posteriores, sistema por sistema.
    ///
    /// Reutilizan DamageResult (sin modificarlo) en vez de duplicar
    /// Damage/IsCrit/HitWeakness/TriggeredBreak.
    /// </summary>
    public readonly struct DamageDealtEvent
    {
        public Combatant Attacker { get; }
        public Combatant Target { get; }
        public DamageResult Result { get; }

        public DamageDealtEvent(Combatant attacker, Combatant target, DamageResult result)
        {
            Attacker = attacker;
            Target = target;
            Result = result;
        }
    }

    public readonly struct DamageReceivedEvent
    {
        public Combatant Target { get; }
        public Combatant Attacker { get; }
        public DamageResult Result { get; }

        public DamageReceivedEvent(Combatant target, Combatant attacker, DamageResult result)
        {
            Target = target;
            Attacker = attacker;
            Result = result;
        }
    }

    public readonly struct SkillUsedEvent
    {
        public Combatant Attacker { get; }
        public SkillData Skill { get; }

        public SkillUsedEvent(Combatant attacker, SkillData skill)
        {
            Attacker = attacker;
            Skill = skill;
        }
    }

    public readonly struct TurnStartEvent
    {
        public Combatant Combatant { get; }

        public TurnStartEvent(Combatant combatant)
        {
            Combatant = combatant;
        }
    }

    public readonly struct TurnEndEvent
    {
        public Combatant Combatant { get; }

        public TurnEndEvent(Combatant combatant)
        {
            Combatant = combatant;
        }
    }

    public readonly struct HealingEvent
    {
        public Combatant Source { get; }
        public Combatant Target { get; }
        public int Amount { get; }

        public HealingEvent(Combatant source, Combatant target, int amount)
        {
            Source = source;
            Target = target;
            Amount = amount;
        }
    }

    public readonly struct CriticalHitEvent
    {
        public Combatant Attacker { get; }
        public Combatant Target { get; }
        public DamageResult Result { get; }

        public CriticalHitEvent(Combatant attacker, Combatant target, DamageResult result)
        {
            Attacker = attacker;
            Target = target;
            Result = result;
        }
    }

    public readonly struct StatusAppliedEvent
    {
        public Combatant Source { get; }
        public Combatant Target { get; }
        public StatusEffect Effect { get; }

        public StatusAppliedEvent(Combatant source, Combatant target, StatusEffect effect)
        {
            Source = source;
            Target = target;
            Effect = effect;
        }
    }

    public readonly struct StatusReceivedEvent
    {
        public Combatant Target { get; }
        public StatusEffect Effect { get; }

        public StatusReceivedEvent(Combatant target, StatusEffect effect)
        {
            Target = target;
            Effect = effect;
        }
    }

    public readonly struct EnemyDefeatedEvent
    {
        public Combatant Combatant { get; }

        public EnemyDefeatedEvent(Combatant combatant)
        {
            Combatant = combatant;
        }
    }

    public readonly struct EnergySpentEvent
    {
        public Combatant Combatant { get; }
        public int Amount { get; }

        public EnergySpentEvent(Combatant combatant, int amount)
        {
            Combatant = combatant;
            Amount = amount;
        }
    }
}
