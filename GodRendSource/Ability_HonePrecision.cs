namespace GodRendSource
{
    public class Ability_HonePrecision : Ability
    {
        public Ability_HonePrecision(Combatant owner) : base(owner, "Hone Precision")
        {
            staminaCost = COST_AVERAGE;

            attribute = new AttributeF(AVERAGE, HIGH, AVERAGE);
        }

        public override PromptResult ChooseTarget(Combatant.Mode mode)
        {
            // Choose yourself by default.
            target = owner;

            return PromptResult.Succeeded;
        }

        public override AbilityResult Execute()
        {
            Message.Narrate(target.name + " focuses deeply.");

            AbilityResult result = AbilityResult.Empty;

            // Check if fail.
            result.didSucceed = Counting.AccuracyCheck((int)(target.support.accuracy * attribute.accuracy)) == false;

            if (result.didSucceed)
            {
                // Check if critical success.
                result.wasCritical = Counting.PrecisionCheck((int)(target.support.precision * attribute.precision), 1);

                int magnitude = (int) (10 * attribute.amplitude);
                magnitude = Counting.MultiplyFlt(magnitude, target.support.amplitude);
                magnitude /= 100;
                if (result.wasCritical)
                {
                    
                    Message.Narrate("Critical success!");
                }

                // Apply bonuses.
                target.IncreaseAttributeAspect(ref target.melee.precision, magnitude);
                target.IncreaseAttributeAspect(ref target.ranged.precision, magnitude);
                target.IncreaseAttributeAspect(ref target.support.precision, magnitude);

                Message.Narrate("Increased melee, ranged, and support precision by " + magnitude + ".");
            }
            else
            {
                Message.Narrate("Failed to focus!");
            }


            return result;
        }
    }
}