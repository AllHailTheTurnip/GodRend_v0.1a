namespace GodRendSource
{
    public class Ability_FirstAid : Ability
    {
        public Ability_FirstAid(Combatant owner) : base(owner, "First Aid")
        {
            attribute = new AttributeF(HIGH, HIGH, AVERAGE);
            staminaCost = COST_LOW;
        }

        public override PromptResult ChooseTarget(Combatant.Mode mode)
        {
            if (mode == Combatant.Mode.Computer)
                return ChooseFriendlyTargetAtRandom();
            
            return ChooseFriendlyTarget();
        }
        
        public override AbilityResult Execute()
        {
            Message.Narrate(owner.name + " goes to " + target.name + " to administer first-aid.");

            bool didFail = Counting.AccuracyCheck((int) (owner.support.accuracy * attribute.accuracy));
            if (!didFail)
            {
                int amountHealed = (int) (50 * Counting.Percentile(owner.support.amplitude) * attribute.amplitude);
                bool didCrit = Counting.PrecisionCheck((int) (owner.support.precision * attribute.precision));
                if (didCrit)
                {
                    Message.Narrate("Critical success!");

                    Counting.MultiplyFlt(ref amountHealed, 1.5f);
                }
                
                owner.RestoreHealth(amountHealed);
                
                Message.Narrate(owner.name + " restored " + amountHealed + " to " + target.name);
            }
            else
            {
                Message.Narrate(owner.name + " failed to apply first-first. No health restored.");
            }

            return AbilityResult.Empty;
        }
    }
}