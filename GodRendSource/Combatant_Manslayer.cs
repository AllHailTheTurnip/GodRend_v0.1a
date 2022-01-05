// Throw Sword (Finished)
// Recall Sword
// Sweep


namespace GodRendSource
{
    public class Combatant_Manslayer : Combatant, ISword
    {
        private bool hasSword = true;

        public Combatant_Manslayer(int team, Mode mode) : base(
            "Manslayer",
            new Health(VITAL_LOW),
            new Stamina(VITAL_HIGH),
            new AttributeI(ATTB_ULTRA_HIGH, ATTB_AVERAGE, ATTB_ULTRA_LOW),
            new AttributeI(ATTB_HIGH, ATTB_VERY_LOW, ATTB_ULTRA_LOW),
            new AttributeI(ATTB_AVERAGE, ATTB_VERY_LOW, ATTB_AVERAGE),
            new Protection(DODGE_VERY_LOW, COVER_VERY_HIGH, ATTR_VERY_HIGH),
            team,
            mode)
        {
            GrantStandardAbilities(this);
            GrantStandardItems(this);
            abilities.Add(new Ability_ThrowSword(this));
            abilities.Add(new Ability_RecallSword(this));
            abilities.Add(new Ability_Sweep(this));
        }


        public bool HasSword()
        {
            return hasSword;
        }

        public void RemoveSword()
        {
            hasSword = false;
        }

        public void GiveSword()
        {
            hasSword = true;
        }
    }
}