namespace GodRendSource
{
    public class Ability_AntiArmorStrike : Ability
    {
        public Ability_AntiArmorStrike(Combatant owner) : base(owner, "AA Strike")
        {
            attribute = new AttributeF(ULTRA_LOW, LOW, LOW);
        }

        public override bool CanUse()
        {
            return owner.specialAmmunition.a > 0;
        }

        public override PromptResult ChooseTarget(Combatant.Mode mode)
        {
            if (mode == Combatant.Mode.Computer)
                return ChooseEnemyTargetAtRandom();
            
            return ChooseEnemyTarget();
        }

        public override AbilityResult Execute()
        {
            AbilityResult result = AbilityResult.Empty;

            // Subtract ammo.
            owner.specialAmmunition.a--;

            // Check if miss?
            CheckHitResult hit = CombatLogic.CheckHit(owner.ranged, target.protection, attribute);
            if (hit.didHit)
            {
                // Check if armor attrition remains and if it wasn't bypass.
                bool isSpecialDamage = target.protection.attrition > 0 && !hit.wasBypass;
                int damage = (int) (attribute.amplitude * owner.ranged.amplitude);

                if (isSpecialDamage)
                {
                    // Check if crit.
                    bool didCrit = Counting.PrecisionCheck((int) (owner.ranged.precision * attribute.precision));
                    if (didCrit)
                    {
                        damage = Counting.MultiplyFlt(damage, 1.5f);
                        Message.Narrate("Critical hit!");
                    }

                    damage *= 3;
                    target.DamageOnlyArmorAttrition(damage);
                    
                    Message.Narrate(damage + " damage inflicted to " + target.NameAndStatus + "'s armor.");
                }
                else
                {
                    CalculateStandardDamage(ref result, hit);
                }
            }
            else
            {
                Message.NarrateMiss(hit, target.protection);
            }

            return result;
        }

        public override string FormalName()
        {
            return name + "(Ammo: " + owner.specialAmmunition.a + "/" + owner.specialAmmunition.b + ")";
        }
    }
}