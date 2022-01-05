/*
 * First Aid (restore health)
 * Restore armor.
 * Increase cover
 */

namespace GodRendSource
{
    public class Combatant_Architect : Combatant
    {
        public Combatant_Architect(int team, Mode mode) : base("Architect",
            new Health(VITAL_AVERAGE),
            new Stamina(VITAL_HIGH),
            new AttributeI(ATTB_LOW, ATTB_AVERAGE, ATTB_HIGH),
            new AttributeI(ATTB_AVERAGE, ATTB_AVERAGE, ATTB_HIGH),
            new AttributeI(ATTB_VERY_HIGH, ATTB_HIGH, ATTB_HIGH),
            new Protection(DODGE_AVERAGE, COVER_LOW, ATTR_HIGH),
            team,
            mode)
        {
            GrantStandardAbilities(this);
            GrantStandardItems(this);
            GrantHealthPotion(this);
            GrantHealthPotion(this);
            
            abilities.Add(new Ability_FirstAid(this));
            abilities.Add(new Ability_FortifyArmor(this));
            abilities.Add(new Ability_IncreaseCover(this));
        }
        
        
    }
}