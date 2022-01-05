namespace GodRendSource
{
    public class Ability_FortifyArmor : Ability
    {
        public Ability_FortifyArmor(Combatant owner) : base(owner, "Fortify Armor")
        {
            attribute = new AttributeF(HIGH, AVERAGE, LOW);
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
            Message.Narrate(owner.name + " attempts to bolster " + target.name + " armor.");

            int acc = (int) (owner.support.accuracy * attribute.accuracy);
            bool didFail = Counting.AccuracyCheck(acc);

            if (!didFail)
            {
                int prec = (int)(owner.support.precision * attribute.precision);
                bool didCrit = Counting.PrecisionCheck(prec);
                
                int bolsterAmount = (int) (Combatant.ATTR_LOW * Counting.Percentile(owner.support.amplitude) * attribute.amplitude);

                if (didCrit)
                {
                    Message.Narrate("Critical success. More armor bolstered.");
                    Counting.MultiplyFlt(ref bolsterAmount, 2f);
                }
                
                Message.Narrate("Successfully increased armor by " + bolsterAmount + " points.");
                target.IncreaseArmorAttrition(bolsterAmount);
            }
            else
            {
                Message.Narrate("Failed to bolster armor.");
            }

            return AbilityResult.Empty;
        }
    }
}