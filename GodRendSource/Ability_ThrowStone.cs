/*
 * Throw a stone at the target.
 * Low accuracy, poor damage, high critical chance.
 */
namespace GodRendSource
{
    public class Ability_ThrowStone : Ability
    {
        public Ability_ThrowStone(Combatant owner) : base(owner, "Throw Stone")
        {
            attribute.accuracy = AVERAGE;
            attribute.amplitude = ULTRA_LOW;
            attribute.precision = HIGH;
        }

        public override AbilityResult Execute()
        {
            AbilityResult result = AbilityResult.Empty;
            Message.Narrate(owner.NameAndStatus + " throws a stone at " + target.NameAndStatus + ".");
            
            // Check hit.
            CheckHitResult checkHitResult = CombatLogic.CheckHit(owner.ranged, target.protection, attribute);
            Message.NarrateHitCheck(checkHitResult, target.protection);
            
            // Calculate and inflict damage.
            CalculateStandardDamage(ref result, checkHitResult);
            
            return result;
        }

        public override PromptResult ChooseTarget(Combatant.Mode mode)
        {
            if (mode == Combatant.Mode.Computer)
                return ChooseEnemyTargetAtRandom();

            return ChooseEnemyTarget();
        }
    }
}