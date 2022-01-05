namespace GodRendSource
{
    public class Ability_RecallSword : Ability, ISword
    {
        private ISword swordOwner;

        public Ability_RecallSword(Combatant owner) : base(owner, "Recall Sword")
        {
            swordOwner = (ISword) owner;
        }

        public bool HasSword()
        {
            return swordOwner.HasSword();
        }

        public void RemoveSword()
        {
            throw new System.NotImplementedException();
        }

        public override PromptResult ChooseTarget(Combatant.Mode mode)
        {
            return TargetSelf();
        }

        public void GiveSword()
        {
            ISword swordOwner = (ISword) owner;
            swordOwner.GiveSword();
        }

        public override AbilityResult Execute()
        {
            Message.Narrate(owner.name + " grasps in the direction of their sword.");

            // Check if failed.
            int acc = (int) (owner.support.accuracy * attribute.accuracy);
            bool didFail = Counting.AccuracyCheck(acc);
            if (!didFail)
            {
                int prec = Counting.MultiplyFlt(owner.support.precision, attribute.precision);
                bool didCrit = Counting.PrecisionCheck(prec);
                if (didCrit)
                {
                    Message.Narrate("Critical success! No stamina spent.");
                    owner.stamina.a += staminaCost;
                }

                Message.Narrate("The sword takes flight and claps firmly in it's master's grip.");
                GiveSword();
            }
            else
            {
                Message.Narrate("Check failed. The sword went nowhere.");
            }


            return AbilityResult.Empty;
        }

        public override bool CanUse()
        {
            return !swordOwner.HasSword();
        }
    }
}