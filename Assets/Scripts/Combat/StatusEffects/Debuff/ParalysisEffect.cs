namespace NAPI.Combat
{
    /// <summary>
    /// El objetivo no puede actuar mientras dure. Se puede asignar
    /// directo a una skill (vía ParalysisDebuffData) o hacer que otro
    /// efecto la genere como proc (ver ParalyzingArcEffect).
    /// </summary>
    public class ParalysisEffect : StatusEffect
    {
        public ParalysisEffect(int duration) : base(duration)
        {
            EffectName = "Parálisis";
        }

        public override bool ShouldSkipHolderTurn(Combatant holder) => true;
    }
}
