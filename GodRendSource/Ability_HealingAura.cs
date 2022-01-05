using System.Collections.Generic;
using System.Data;

namespace GodRendSource
{
    public class Ability_HealingAura : Ability
    {
        public List<Combatant> targets = new List<Combatant>();

        public Ability_HealingAura(Combatant owner) : base(owner, "Healing Aura")
        {
            staminaCost = COST_AVERAGE;
            attribute = new AttributeF(ULTRA_LOW, VERY_HIGH, AVERAGE);
        }

        public override AbilityResult Execute()
        {
            AbilityResult result = AbilityResult.Empty;

            // Narration.
            Message.Narrate(owner.name + " glows bright with a healing aura.");
            
            // Target each allie.
            foreach (Combatant allie in targets)
            {
                int amountHealed = Counting.MultiplyFlt(owner.support.amplitude, attribute.amplitude);
                string resultMessage = owner.name + " targets " + allie.name + ". ";
                
                // Test if miss (accuracy)
                int supportAccIF = Counting.MultiplyFlt(owner.support.accuracy, attribute.accuracy);
                bool failed = CombatLogic.CheckAgainstValue("Accuracy",supportAccIF, false, 5);

                if (!failed)
                {
                    // Test if crit (precision)
                    int supportPrecIF = Counting.MultiplyFlt(owner.support.precision, attribute.precision);
                    bool critical = CombatLogic.CheckAgainstValue("Precision",supportPrecIF, true, 95);
                    if (critical)
                    {
                        resultMessage += "Critical success! ";
                        Counting.MultiplyFlt(ref amountHealed, 1.5f);
                    }

                    // Apply healing.
                    resultMessage += amountHealed + " restored to health.";
                    allie.RestoreHealth(amountHealed);
                }
                else
                {
                    resultMessage += "But misses.";
                    result.didSucceed = false;
                }
                
                Message.Narrate(resultMessage);
            }

            return result;
        }

        public override PromptResult ChooseTarget(Combatant.Mode mode)
        {
            Message.Narrate(owner.name + " targets theirself and all their remaining allies.");

            // Select all allies of same team.
            targets = Combatant.AllByTeam(owner.team);

            return PromptResult.Succeeded;
        }
    }
}