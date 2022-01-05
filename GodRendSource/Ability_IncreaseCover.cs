namespace GodRendSource
{
    public class Ability_IncreaseCover : Ability
    {
        public Ability_IncreaseCover(Combatant owner) : base(owner, "Increase Cover")
        {
            attribute = new AttributeF(AVERAGE, AVERAGE, HIGH);
            staminaCost = COST_HIGH;
        }

        public override PromptResult ChooseTarget(Combatant.Mode mode)
        {
            if (mode == Combatant.Mode.Computer)
                return ChooseFriendlyTargetAtRandom();
            
            return ChooseFriendlyTarget();
        }

        public override AbilityResult Execute()
        {
            int acc = (int) (owner.support.accuracy * attribute.accuracy);
            bool didFail = Counting.AccuracyCheck(acc);
            if (!didFail)
            {
                Message.Narrate(owner.name + " attempts to apply more armor to " + target.name);
                
                // Instance initial gain.
                int gainedCover = (int)(10 * Counting.Percentile(owner.support.amplitude) * attribute.amplitude);
                
                // Test crit.
                int prec = (int) (owner.support.precision * attribute.precision);
                bool didCrit = Counting.PrecisionCheck(prec);
                if (didCrit)
                {
                    Message.Narrate("Critical success!");
                    Counting.MultiplyFlt(ref gainedCover, 1.5f);
                }
                
                // Apply armor.
                Message.Narrate(target.name + " gains " + gainedCover + " points of cover.");
                target.IncreaseArmorCover(gainedCover);
            }
            else
            {
                Message.Narrate("Failed to apply extra armor.");
            }

            return AbilityResult.Empty;
        }
    }
}