/*
 * High Accuracy.
 * Abilities...
 * ... Improve crit-chance. (Completed)
 * ... Anti-armor round (High damage against armor, low damage against body)
 * ... Crossbow (3 uses)
 */

namespace GodRendSource
{
    public class Combatant_Accutron : Combatant
    {
        public Combatant_Accutron(int team, Mode mode) : base("Accutron", new Health(100),
            new Stamina(100),
            new AttributeI(ATTB_VERY_LOW, ATTB_VERY_HIGH, ATTB_AVERAGE),
            new AttributeI(ATTB_AVERAGE, ATTB_ULTRA_HIGH, ATTB_HIGH),
            new AttributeI(),
            new Protection(DODGE_AVERAGE, COVER_LOW, ATTR_AVERAGE),
            team,
            mode)
        {

            GrantStandardAbilities(this);
            GrantStandardItems(this);

            specialAmmunition = new Couple<int>(5, 5);

            Ability_HonePrecision honePrecision = new Ability_HonePrecision(this);
            abilities.Add(honePrecision);

            Ability_AntiArmorStrike aaStrike = new Ability_AntiArmorStrike(this);
            abilities.Add(aaStrike);

            Ability_Crossbow crossbow = new Ability_Crossbow(this);
            abilities.Add(crossbow);
        }
    }
}