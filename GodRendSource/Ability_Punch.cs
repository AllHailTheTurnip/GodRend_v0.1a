using System;
using System.Collections.Generic;
using static System.Console;
/*
 * Punch the target.
 * High accuracy, low damage, low critical chance.
 */

namespace GodRendSource
{
    public class Ability_Punch : Ability
    {
        public Ability_Punch(Combatant owner) : base(owner, "Punch")
        {
            attribute.amplitude = VERY_LOW;
            attribute.accuracy = HIGH;
            attribute.precision = LOW;
        }

        public override AbilityResult Execute()
        {
            Message.Narrate(owner.NameAndStatus + " attempts to punch " + target.NameAndStatus + ".");

            return ExecuteStandardAttack(owner.melee);
        }

        
        public override PromptResult ChooseTarget(Combatant.Mode mode)
        {
            if (mode == Combatant.Mode.Computer)
                return ChooseEnemyTargetAtRandom();
            
            return ChooseEnemyTarget();
        }
    }
}

