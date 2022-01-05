using System;

namespace GodRendSource
{
    public static class CombatLogic
    {
        public static int CalculateEffectiveness(int combatantAmplitude, float abilityAmplitude, bool wasCrit)
        {
            int effectiveness = combatantAmplitude;

            // Increase if critical-hit.
            if (wasCrit)
                effectiveness = (int) (effectiveness * 1.5f);

            // Increase by base amplitude.
            Counting.MultiplyFlt(ref effectiveness, abilityAmplitude);

            return effectiveness;
        }


        public static void UpdateStatusEffects(Combatant target)
        {
            for (int i = 0; i < target.statusEffects.Count; i++)
            {
                Status status = target.statusEffects[i];

                status.ApplyEffect();
                status.ReduceDuration();

                if (status.IsExpired)
                {
                    // Remove any applied effects and then
                    // remove the status from the list.
                    status.RemoveEffect();
                    target.statusEffects.Remove(status);
                }
            }
        }

        public static bool CheckAgainstValue(string type, int range, bool isGreaterThan, int value)
        {
            int roll = Counting.ExplodingDie(type, range);
            if (isGreaterThan)
                return roll > value;
            return roll <= value;
        }

        public static bool CheckAgainstdodge(int accuracy, int dodge)
        {
            return CheckAgainstValue("Accuracy",  accuracy, true, dodge);
        }

        public static CheckHitResult CheckHit(
            AttributeI offense,
            Protection defense,
            AttributeF baseAttribute,
            bool autoHit = false,
            bool autoCrit = false)
        {
            bool didHit = false;
            bool wasCrit = false;
            bool didBypass = false;

            // Get offender info.
            int attackerAcc = offense.accuracy;
            int attackerPrc = offense.precision;

            // Get protection info defending combatant.
            int dodge = defense.dodge;
            int cover = defense.cover;

            // Roll for accuracy.
            int accCheck = autoHit ? dodge : Counting.ExplodingDie("Accuracy", attackerAcc);
            Counting.MultiplyFlt(ref accCheck, baseAttribute.accuracy);
            if (accCheck > dodge)
            {
                didHit = true;
                
                // Check if bypassed.
                if (accCheck > cover)
                {
                    Message.Narrate("Bypassed!");
                    didBypass = true;
                }
                // Roll for critical.
                int critCheck = autoCrit ? 999 : Counting.ExplodingDie("Precision", attackerPrc);
                Counting.MultiplyFlt(ref critCheck, baseAttribute.precision);
                int critThreshold = 90;
                if (critCheck >= critThreshold)
                {
                    wasCrit = true;
                }
            }
            
            return new CheckHitResult(accCheck, didHit, wasCrit, didBypass);
        }
    }
}