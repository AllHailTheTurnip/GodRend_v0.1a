

namespace GodRendSource
{
    public class Ability_Smokebomb : Ability
    {
        public Ability_Smokebomb(Combatant owner) : base(owner, "Smoke Bomb")
        {
            attribute = new AttributeF(VERY_HIGH);
            staminaCost = COST_HIGH;
            
            
        }

        public override PromptResult ChooseTarget(Combatant.Mode mode)
        {
            return TargetSelf();
        }

        public override AbilityResult Execute()
        {
            AbilityResult result = AbilityResult.Empty;
            
            Message.Narrate(owner.name + " throws a smokebomb at their feet.");
            // Creates the "Smoke Cloud" status effect; temporarily doubles your dodge.
            // Checks if failed.
            bool didFail = Counting.AccuracyCheck(owner.support.accuracy);
            
            if(!didFail)
            {
                float bonus = 1;
                
                // Checks if crit'd.
                bool didCrit = Counting.PrecisionCheck(owner.support.precision);
                if (didCrit)
                {
                    Message.Narrate("Critical success; effectiveness doubled.");
                    bonus *= 2;
                }
                
                // Applies effect.
                owner.InflictStatusHidden(2, attribute.amplitude * bonus);
            }
            else
            {
                Message.Narrate(owner.name + " somehow failed to throw a smokebomb at their feet.");
            }

            return result;
        }
    }
}