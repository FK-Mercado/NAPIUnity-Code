using NAPI.Data;

namespace NAPI.Combat
{
    public class PlayerSkillSelectedEvent
    {
        public Combatant Combatant { get; }
        public SkillData Skill { get; }

        public PlayerSkillSelectedEvent(
            Combatant combatant,
            SkillData skill)
        {
            Combatant = combatant;
            Skill = skill;
        }
    }
}