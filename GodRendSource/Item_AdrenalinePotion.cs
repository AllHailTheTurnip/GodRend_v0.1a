namespace GodRendSource
{
    public class Item_AdrenalinePotion : Item
    {
        public Item_AdrenalinePotion()
        {
            name = "Adrenaline Potion";
            description = "Increases melee amplitude and reduces ranged accuracy upon consumption.";
        }

        public override void ApplyToTarget(Combatant combatant)
        {
            int meleeGain = 10;
            int rangeLoss = 10;

            // Accuracy poses chance to *drop* the potion.
            bool dropItem = TestIfDropItem(combatant, ref meleeGain, ref rangeLoss);

            // Precision poses chance to get 50% more of the effects.
            if (!dropItem)
                TestIfCritItemProCon(combatant, ref meleeGain, ref rangeLoss);

            Message.Narrate(combatant.name + " gains " + meleeGain + " Melee:Amplitude, and loses " + rangeLoss +
                            " Ranged:Accuracy.");

            combatant.IncreaseAttributeAspect(ref combatant.melee.amplitude, meleeGain);
            combatant.DecreaseAttributeAspect(ref combatant.ranged.accuracy, rangeLoss);
        }
    }
}