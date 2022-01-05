namespace GodRendSource
{
    public class Ability_RapidPunch : Ability
    {
        public Ability_RapidPunch(Combatant owner) : base(owner, "Rapid Punch")
        {
            attribute = new AttributeF(VERY_LOW, HIGH, LOW);
            staminaCost = COST_AVERAGE;
        }

        public override PromptResult ChooseTarget(Combatant.Mode mode)
        {
            if (mode == Combatant.Mode.Computer)
                return ChooseEnemyTargetAtRandom();
            
            return ChooseEnemyTarget();
        }

        public override AbilityResult Execute()
        {
            Message.Narrate(owner.name + " unleashes a flurry of blows against " + target.name);
            
            int punches = 0;
            int curve = 3;
            for(int i = 0; i < curve; i++)
            {
                punches += Counting.random.Next(6) + 1;
            }
            punches /= curve;

            for (int i = 0; i < punches; i++)
            {
                ExecuteStandardAttack(owner.melee);    
            }

            return AbilityResult.Empty;
        }
    }
}