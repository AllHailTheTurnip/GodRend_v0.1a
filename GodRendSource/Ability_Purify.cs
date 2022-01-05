namespace GodRendSource
{
    public class Ability_Purify : Ability
    {
        public Ability_Purify(Combatant owner) : base(owner, "Purify")
        {
            attribute = new AttributeF(AVERAGE, ULTRA_HIGH, AVERAGE);
            staminaCost = COST_HIGH;
        }

        public override AbilityResult Execute()
        {
            Message.Narrate(owner.name + " focuses their inner energies.");

            bool didFail = Counting.AccuracyCheck(owner.support.accuracy);
            if (!didFail)
            {
                bool didCrit = Counting.PrecisionCheck(owner.support.precision);
                if (didCrit)
                {
                    int regainedHealth = (int) (25 * Counting.Percentile(owner.support.amplitude) * attribute.amplitude);
                    Message.Narrate("Critical success! Regained " + regainedHealth + " health.");
                    
                    owner.RestoreHealth(regainedHealth);
                }
                
                Message.Narrate(owner.name + " removes all status effects from their self.");
                owner.RemoveAllStatusEffects();
            }
            else
            {
                Message.Narrate(owner.name +"'s concentration broke.");
            }

            return AbilityResult.Empty;
        }

        public override PromptResult ChooseTarget(Combatant.Mode mode)
        {
            return TargetSelf();
        }
    }
}