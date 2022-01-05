namespace GodRendSource
{
    public class Ability_ThrowingStars : Ability
    {
        public Ability_ThrowingStars(Combatant owner) : base(owner, "Throwing Stars")
        {
            attribute = new AttributeF(ULTRA_LOW, AVERAGE, LOW);
            staminaCost = COST_AVERAGE;
        }

        public override PromptResult ChooseTarget(Combatant.Mode mode)
        {
            if (mode == Combatant.Mode.Computer)
                return ChooseEnemyTargetAtRandom();
            
            return ChooseEnemyTarget();
        }

        public override AbilityResult Execute()
        {
            Message.Narrate(owner.name + " throws a handful of throwing-stars at " + target.name);
            
            ExecuteStandardAttack(owner.ranged);
            ExecuteStandardAttack(owner.ranged);
            ExecuteStandardAttack(owner.ranged);

            return AbilityResult.Empty;
        }
    }
}