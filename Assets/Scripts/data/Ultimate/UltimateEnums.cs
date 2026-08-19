namespace NAPI.Data
{
    /// <summary>
    /// Tipos de eventos que pueden generar carga de Ultimate.
    /// La lógica que dispara cada evento se conectará después al combate.
    /// </summary>
    public enum UltimateChargeEventType
    {
        DamageReceived,
        DamageDealt,
        SkillUsed,
        TurnStart,
        TurnEnd,
        HealingReceived,
        HealingPerformed,
        CriticalHit,
        StatusApplied,
        StatusReceived,
        EnemyDefeated,
        EnergySpent
    }

    /// <summary>
    /// Operadores utilizados por las condiciones de una Ultimate.
    /// </summary>
    public enum UltimateConditionOperator
    {
        GreaterThanOrEqual,
        GreaterThan,
        Equal,
        LessThanOrEqual,
        LessThan,
        NotEqual
    }

    /// <summary>
    /// Tipos de condiciones que una Ultimate puede exigir para poder lanzarse.
    /// El evaluador de condiciones se implementará posteriormente.
    /// </summary>
    public enum UltimateConditionType
    {
        EnergyPercentage,
        HPPercentage,
        TurnCount,
        HasStatusEffect,
        HasBuff,
        HasDebuff,
        AliveAllies,
        AliveEnemies
    }

    /// <summary>
    /// Determina cómo se consume la CAR al lanzar un punto de Ultimate.
    /// </summary>
    public enum UltimateChargeConsumptionMode
    {
        ResetToZero,
        SpendRequiredCharge,
        SpendCustomAmount
    }
}
