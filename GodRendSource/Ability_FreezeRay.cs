namespace GodRendSource
{
    public class Ability_FreezeRay : Ability
    {
        public Ability_FreezeRay(Combatant owner) : base(owner, "Freeze Ray")
        {
            attribute = new AttributeF(ULTRA_LOW, AVERAGE, HIGH);
            staminaCost = 25;
        }

        public override PromptResult ChooseTarget(Combatant.Mode mode)
        {
            if (mode == Combatant.Mode.Computer)
                return ChooseEnemyTargetAtRandom();
            
            return ChooseEnemyTarget();
        }

        public override AbilityResult Execute()
        {
            // Narrate action.
            Message.Narrate(owner.name + " unleashes a blast of ice at " + target.name);

            AbilityResult result = AbilityResult.Empty;

            // Test hit and crit based on accuracy.
            CheckHitResult checkHitResult =
                CombatLogic.CheckHit(owner.ranged, target.protection, attribute);
            Message.NarrateHitCheck(checkHitResult, target.protection);

            // Calculate damage based on accuracy, precision, and amplitude.
            CalculateStandardDamage(ref result, checkHitResult, false);
            
            // Check if was crit.
            if (checkHitResult.wasCritical && checkHitResult.didHit)
            {
                Message.Narrate(target.name + " is frozen! Dodge halved.");
                target.InflictStatusFreeze(2);
            }


            return result;
        }
    }
}