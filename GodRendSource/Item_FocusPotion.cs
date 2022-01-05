namespace GodRendSource
{
    public class Item_FocusPotion : Item
    {
        public Item_FocusPotion()
        {
            name = "Focus Potion";
            description = "Improves your accuracy but reduces your melee amplitude upon consumption.";
        }

        public override void ApplyToTarget(Combatant combatant)
        {
            int rangedGain = 10;
            int meleeLoss = 10;
            
            // Check if dropped.
            TestIfDropItem(combatant, ref rangedGain, ref meleeLoss);
            
            // Check if crit'd.
            TestIfCritItemProCon(combatant, ref rangedGain, ref meleeLoss);

            Message.Narrate(combatant.name +" gains " + rangedGain + " Ranged:Accuracy, and loses " + meleeLoss + " Melee:Amplitude.");
            Counting.IncrementWithCeiling(ref combatant.ranged.accuracy, 10, 150);
            Counting.DecrementWithFloor(ref combatant.melee.amplitude, 10, 50);
        }
    }
}