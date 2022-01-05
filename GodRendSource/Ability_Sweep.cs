using System.Collections.Generic;

namespace GodRendSource
{
    public class Ability_Sweep : Ability
    {
        private ISword swordOwner;
        private List<Combatant> targets;

        public Ability_Sweep(Combatant owner) : base(owner, "Sword Sweep")
        {
            swordOwner = (ISword) owner;
            attribute = new AttributeF(AVERAGE, LOW, LOW);
            staminaCost = COST_VERY_HIGH;
        }

        public override AbilityResult Execute()
        {
            Message.Narrate(owner.name + " makes a vast sweep with their sword.");

            // Make an attack against every enemy.
            foreach (Combatant curTarget in targets)
            {
                target = curTarget;
                Message.Narrate("The sweep passes at " + target.name);
                ExecuteStandardAttack(owner.melee);
            }

            return AbilityResult.Empty;
        }

        public override PromptResult ChooseTarget(Combatant.Mode mode)
        {
            // Target every enemy combatant.
            targets = Combatant.AllByDifferentTeam(owner.team);

            return PromptResult.Succeeded;
        }

        public override bool CanUse()
        {
            return swordOwner.HasSword() && base.CanUse();
        }
    }
}