/*
 * Rapid Punch  (Finished)
 * Doom Strike  (Finished)
 * Purify       (Finished)
 */

namespace GodRendSource
{
    public class Combatant_Chimera : Combatant
    {
        public Combatant_Chimera(int team, Mode mode) : base("Chimera",
            new Health(VITAL_HIGH),
            new Stamina(VITAL_AVERAGE),
            new AttributeI(),
            new AttributeI(ATTB_LOW),
            new AttributeI(ATTB_HIGH, ATTB_HIGH),
            new Protection(DODGE_HIGH, COVER_AVERAGE, ATTR_AVERAGE),
            team,
            mode)
        {
            GrantStandardAbilities(this);
            GrantStandardItems(this);

            Ability_RapidPunch punch = new Ability_RapidPunch(this);
            abilities.Add(punch);

            Ability_DoomStrike doomStrike = new Ability_DoomStrike(this);
            abilities.Add(doomStrike);

            Ability_Purify purify = new Ability_Purify(this);
            abilities.Add(purify);
        }
    }
}