

using System;

namespace GodRendSource
{
    public abstract class Item
    {
        public string name;
        public string description;

        public virtual void ApplyToTarget(Combatant combatant)
        {
            throw new Exception("Not implemented!");
        }

        public bool TestIfDropItem(Combatant combatant, ref int amount, int failThreshold = 5)
        {
            bool didDrop = Counting.AccuracyCheck(combatant.support.accuracy, failThreshold);
            if (didDrop)
            {
                Message.Narrate(combatant.name + " dropped (Oops!)" + name + ".");
                amount = 0;
                return true;
            }

            return false;
        }


        public bool TestIfDropItem(Combatant combatant, ref int amount1, ref int amount2,
            int failThreshold = 5)
        {
            bool didDrop = Counting.AccuracyCheck(combatant.support.accuracy, failThreshold);
#if TestAutoDrop
            didDrop = true;
#endif
            if (didDrop)
            {
                Message.Narrate(combatant.name + " dropped (Oops!)" + name + ".");
                amount1 = 0;
                amount2 = 0;
                return true;
            }

            return false;
        }

        public bool TestIfCritItem(Combatant combatant, ref int amount, float increase = 1.5f, int critThreshold = 95)
        {
            bool didDrop = Counting.PrecisionCheck(combatant.support.accuracy, critThreshold);
            if (didDrop)
            {
                Message.Narrate("The item is critically effective!");
                Counting.AdjustByPercentile(ref amount, increase);
                return true;
            }

            return false;
        }

        public bool TestIfCritItem(Combatant combatant, ref int amount1, ref int amount2, float increase = 1.5f,
            int critThreshold = 95)
        {
            bool didDrop = Counting.PrecisionCheck(combatant.support.accuracy, critThreshold);
            if (didDrop)
            {
                Message.Narrate("The item is critically effective!");
                Counting.AdjustByPercentile(ref amount1, increase);
                Counting.AdjustByPercentile(ref amount2, increase);
                return true;
            }

            return false;
        }


        public bool TestIfCritItemProCon(Combatant combatant, ref int pro, ref int con, float increase = 1.5f,
            float reduction = 0.5f, int critThreshold = 95)
        {
            bool didCrit = Counting.PrecisionCheck(combatant.support.accuracy, critThreshold);
#if TestCritAutoPass
            didCrit = true;
#endif
            if (didCrit)
            {
                Message.Narrate("The item is critically effective!");
                Counting.AdjustByPercentile(ref pro, increase);
                Counting.AdjustByPercentile(ref con, reduction);
                return true;
            }

            return false;
        }
    }
}