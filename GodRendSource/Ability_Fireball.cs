namespace GodRendSource
{
    public class Ability_Fireball : Ability
    {
        public Ability_Fireball(Combatant owner) : base(owner, "Fireball")
        {
            attribute = new AttributeF(AVERAGE, VERY_HIGH, LOW);
            staminaCost = COST_HIGH;
        }

        public override AbilityResult Execute()
        {
            Message.Narrate(owner.NameAndStatus + " strikes " + target.NameAndStatus + " with a ball of fire.");
            
            return ExecuteStandardAttack(owner.ranged, true);
        }

        public override PromptResult ChooseTarget(Combatant.Mode mode)
        {
            if (mode == Combatant.Mode.Computer)
                return ChooseEnemyTargetAtRandom();
            
            return ChooseEnemyTarget();
        }
    }
}