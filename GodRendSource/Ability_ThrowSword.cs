using System;
using System.Diagnostics.Contracts;

namespace GodRendSource
{
    public class Ability_ThrowSword : Ability, ISword
    {
        public Ability_ThrowSword(Combatant owner) : base(owner, "Throw Sword")
        {
            attribute = new AttributeF(AVERAGE, AVERAGE, ULTRA_HIGH);
            staminaCost = COST_AVERAGE;
        }

        public bool HasSword()
        {
            try
            {
                ISword swordOwner = (ISword) owner;
                return swordOwner.HasSword();
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public void RemoveSword()
        {
            ISword swordOwner = (ISword) owner;
            swordOwner.RemoveSword();
        }

        public void GiveSword()
        {
            throw new NotImplementedException();
        }

        public override PromptResult ChooseTarget(Combatant.Mode mode)
        {
            if (mode == Combatant.Mode.Computer)
                return ChooseEnemyTargetAtRandom();
            
            return ChooseEnemyTarget();
        }

        public override bool CanUse()
        {
            return HasSword();
        }

        public override AbilityResult Execute()
        {
            // Check if missed.
            /*int acc = (int)(owner.ranged.accuracy * attribute.accuracy);
            bool didHit = CombatLogic.CheckAgainstdodge(acc, target.protection.dodge);
            if (didHit)
            {
                Message.Narrate("Successful hit!");

                int inflictedDamage = (int) (50 * Counting.Percentile(owner.ranged.amplitude) * attribute.amplitude);
                
                // Test if critical.
                int prec = (int)(owner.ranged.precision * attribute.precision);
                bool didCrit = Counting.CheckHigh(prec);
                if (didCrit)
                {
                    Message.Narrate("Critical damage!");
                    Counting.MultiplyFlt(ref inflictedDamage, 1.5f);
                }
                
                // Check bypass.
                
                
                Message.Narrate("Inflicted " + inflictedDamage + " to " + target.name);
                
            }
            else
            {
                Message.Narrate("The sword missed without spilling a drop of blood!");
            }*/

            Message.Narrate(owner.name + " throws their sword at " + target.name);
            ExecuteStandardAttack(owner.ranged);
            RemoveSword();

            return AbilityResult.Empty;
        }
    }
}