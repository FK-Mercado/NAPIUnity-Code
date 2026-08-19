namespace NAPI.Data
{
    public enum ElementType
    {
        Fisico,
        Fuego,
        Natura,
        Rayo,
        Hielo,
        aqua,
        Oscuridad,
        Lux
    }

    public enum SkillType
    {
        AtaqueBasico,
        HabilidadBasica,
        HabilidadAvanzada,
        Definitivo
    }

    public enum TargetType
    {
        UnEnemigo,
        TodosLosEnemigos,
        UnAliado,
        TodosLosAliados,
        Uno_Mismo
    }

    public enum AIBehaviour
    {
        Agresivo,
        Defensivo,
        Soporte,
        Aleatorio
    }

    public enum ItemType
    {
        Curacion,
        Daño,
        DebuffEnemigo,
        BuffAliado
    }

    public enum Rarity
    {
        Comun,
        Raro,
        Epico,
        Legendario
    }

    /// <summary>
    /// Nuevo: tipo de alcance de una skill. Lo necesitan varios efectos
    /// de Oscuridad y Agua (Campo de Vacío, Barrera de Fluido, Soften
    /// Defense) para distinguir cuerpo a cuerpo de a distancia.
    /// </summary>
    public enum AttackRangeType
    {
        Melee,
        Ranged
    }
}
