using System;

namespace GodRendSource
{
    public class Item_HealthPotion : Item
    {
        public Item_HealthPotion()
        {
            name = "Health Potion";
            description = "Restores an amount of health upon consumption.";
        }

        public override void ApplyToTarget(Combatant combatant)
        {
            int amount = 50;
            Counting.AdjustByPercentile(ref amount, combatant.support.amplitude);

            // Accuracy poses a chance to drop (health gained = 0) the potion.
            bool didDrop = TestIfDropItem(combatant, ref amount);

            // Precision poses a change to increase health gained by 50%.
            if (!didDrop) 
                TestIfCritItem(combatant, ref amount);

            combatant.RestoreHealth(amount);
            Message.Narrate(combatant.name + " regains " + amount + " health " + combatant.health + ".");
        }
    }
}