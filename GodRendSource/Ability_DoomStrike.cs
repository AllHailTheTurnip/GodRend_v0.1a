namespace GodRendSource
{
    public class Ability_DoomStrike : Ability
    {
        public Ability_DoomStrike(Combatant owner) : base(owner, "Doom Strike")
        {
            attribute = new AttributeF(MINIMUM, AVERAGE, AVERAGE);
            staminaCost = COST_HIGH;
        }

        public override PromptResult ChooseTarget(Combatant.Mode mode)
        {
            if (mode == Combatant.Mode.Computer)
                return ChooseEnemyTargetAtRandom();
            
            return ChooseEnemyTarget();
        }

        public override AbilityResult Execute()
        {
            Message.Narrate(owner.name + " focuses their dark impulses and strikes at " + target.name);
            bool didFail = Counting.AccuracyCheck(owner.melee.accuracy);
            if (!didFail)
            {
                int doomClock = 4;
                
                bool didCrit = Counting.PrecisionCheck(owner.melee.precision);
                if (didCrit)
                {
                    Message.Narrate("Critical success!");
                    
                    doomClock = 2;
                }
                
                Message.Narrate(owner.name + " has cursed " + target.name + " with doom.");
                target.InflictStatusDoom(doomClock);
            }
            else
            {
                Message.NarrateMiss();
            }

            return AbilityResult.Empty;
        }
    }
}