namespace GodRendSource
{
    public class Ability_Crossbow : Ability
    {
        public Ability_Crossbow(Combatant owner) : base(owner, "Crossbow")
        {
            attribute = new AttributeF(AVERAGE, VERY_HIGH, LOW);
        }

        public override bool CanUse()
        {
            return owner.specialAmmunition.a > 0;
        }

        public override string FormalName()
        {
            return name + "(Ammo: " + owner.specialAmmunition.a + "/" + owner.specialAmmunition.b + ")";
        }

        public override PromptResult ChooseTarget(Combatant.Mode mode)
        {
            if (mode == Combatant.Mode.Computer)
                return ChooseEnemyTargetAtRandom();
            
            return ChooseEnemyTarget();
        }

        public override AbilityResult Execute()
        {
            // Subtract ammunition.
            owner.specialAmmunition.a--;
            
            return ExecuteStandardAttack(owner.ranged);
        }
    }
}