/* Ninja - sneaky and fast!
 * Throwing Stars (three checks for one attack) - Completed.
 * Smokebomb (Free successful dodge)
 * Shadow Clone (Make copy of self?)
 */

using static GodRendSource.Counting;
namespace GodRendSource
{
    public class Combatant_Raiden : Combatant
    {
        
        public Combatant_Raiden(int team, Mode mode, float scoreModifier = 1.0f) 
            : base("Raiden", new Health(VITAL_LOW) * scoreModifier,
                new Stamina(VITAL_HIGH) * scoreModifier, 
                new AttributeI(ATTB_LOW, ATTB_AVERAGE, ATTB_HIGH), 
                new AttributeI(ATTB_AVERAGE, ATTB_HIGH, ATTB_LOW), 
                new AttributeI(ATTB_HIGH, ATTB_AVERAGE, ATTB_HIGH), 
                new Protection(DODGE_VERY_HIGH, COVER_VERY_LOW, ATTR_LOW), team, mode)
        {
            GrantStandardAbilities(this);
            GrantStandardItems(this);

            Ability_ThrowingStars throwingStars = new Ability_ThrowingStars(this);
            abilities.Add(throwingStars);

            Ability_Smokebomb smokebomb = new Ability_Smokebomb(this);
            abilities.Add(smokebomb);

            Ability_ShadowClone shadowClone = new Ability_ShadowClone(this);
            abilities.Add(shadowClone);
            
        }
    }
}