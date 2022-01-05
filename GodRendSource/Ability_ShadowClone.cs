namespace GodRendSource
{
    public class Ability_ShadowClone : Ability
    {
        public Ability_ShadowClone(Combatant owner) : base(owner, "Shadow Clone")
        {
            staminaCost = COST_VERY_HIGH;
        }

        public override PromptResult ChooseTarget(Combatant.Mode mode)
        {
            return TargetSelf();
        }

        public override AbilityResult Execute()
        {
            AbilityResult result = AbilityResult.Empty;

            // Narrate.
            Message.Narrate(owner.name + " focuses their chakra to create a clone of themself.");

            // Check if failed.
            bool didFail = Counting.AccuracyCheck(owner.support.accuracy);
            if (!didFail)
            {
                float variation = 0.5f;

                // Check if crit.
                bool didCrit = Counting.PrecisionCheck(owner.support.precision);
                if (didCrit)
                {
                    Message.Narrate("Critical success! Clone is stronger.");
                    variation = 0.75f;
                }

                Message.Narrate(name + " created a clone of themselves.");
                Combatant_Raiden raidenClone = new Combatant_Raiden(owner.team, Combatant.Mode.Computer, variation);
            }
            else
            {
                Message.Narrate("But failed; no clone appeared.");
            }

            return result;
        }
    }
}