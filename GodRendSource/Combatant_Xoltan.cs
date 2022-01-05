/*
 * Can do... Abilities use stamina... A lot of stamina.
 * Fireball/MagicMissile (Auto-hit)
 * Icebeam - Very low damage, chance to freeze target.
 * Healing ability of some kind.
 */

namespace GodRendSource
{
    public class Combatant_Xoltan : Combatant
    {
        public Combatant_Xoltan(int team, Mode mode) :base(
            "Xoltan",
            new Health(VITAL_LOW),
            new Stamina(VITAL_VERY_HIGH),
            new AttributeI(ATTB_VERY_LOW, ATTB_LOW, ATTB_AVERAGE),
            new AttributeI(ATTB_AVERAGE, ATTB_VERY_HIGH, ATTB_AVERAGE),
            new AttributeI(ATTB_AVERAGE, ATTB_AVERAGE, ATTB_LOW),
            new Protection(DODGE_AVERAGE, COVER_LOW, ATTR_LOW),
            team, mode)
        {
            GrantStandardAbilities(this);
            GrantStandardItems(this);
            
            Ability_Fireball fireball = new Ability_Fireball(this);
            abilities.Add(fireball);

            Ability_FreezeRay freezeRay = new Ability_FreezeRay(this);
            abilities.Add(freezeRay);

            Ability_HealingAura healingAura = new Ability_HealingAura(this);
            abilities.Add(healingAura);
        }
    }
}