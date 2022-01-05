using System;
using System.Collections.Generic;

namespace GodRendSource
{
    public class Ability
    {
        public string name;
        public Combatant owner;
        public Combatant target;
        public int staminaCost = 0;

        protected AttributeF attribute = new AttributeF();

        public const float
            MINIMUM = 0.0f,
            ULTRA_LOW = 0.10f,
            VERY_LOW = 0.25f,
            LOW = 0.5f,
            AVERAGE = 1f,
            HIGH = 1.50f,
            VERY_HIGH = 2.0f,
            ULTRA_HIGH = 2.5f,
            MAX = 3.0f;

        public const int COST_AVERAGE = 25;
        public const int COST_LOW = 13;
        public const int COST_VERY_LOW = 6;
        public const int COST_HIGH = 50;
        public const int COST_VERY_HIGH = 100;

        public virtual AbilityResult Execute()
        {
            throw new Exception("Action not implemented!");
        }

        public virtual string FormalName()
        {
            if (staminaCost == 0)
                return name;
            else
            {
                return name + "(" + staminaCost + ")";
            }
        }

        public Ability(Combatant owner, string name)
        {
            this.name = name;
            this.owner = owner;
        }

        public virtual PromptResult ChooseTarget(Combatant.Mode mode)
        {
            throw new Exception("Not implemented!");
        }


        public override string ToString()
        {
            return name;
        }

        public virtual bool CanUse()
        {
            // By default, checks stamina.
            if (staminaCost <= owner.stamina.a)
                return true;

            return false;
        }

        protected void PrintEnemyTargets()
        {
            foreach (var combatant in Combatant.AllByDifferentTeam(owner.team))
            {
                if (combatant.IsAlive)
                    Message.Narrate(" - " + combatant.NameAndStatus);
            }
        }

        protected void PrintOwnTeamTargets()
        {
            foreach (var combatant in Combatant.AllBySameTeam(owner.team))
            {
                if (combatant.IsAlive)
                    Message.Narrate(" - " + combatant.NameAndStatus);
            }
        }

        protected PromptResult TryGetCombatantByName(string name, out Combatant potentialTarget)
        {
            potentialTarget = Combatant.GetByName(name);
            if (potentialTarget == null)
            {
                return PromptResult.Failed("Could not find target by name of '" + name + "'.");
            }

            return PromptResult.Succeeded;
        }

        public AbilityResult ExecuteStandardAttack(AttributeI combatantAttribute, bool autohit = false)
        {
            AbilityResult result = AbilityResult.Empty;

            // Test hit and crit based on accuracy.
            CheckHitResult checkHitResult =
                CombatLogic.CheckHit(combatantAttribute, target.protection, attribute, autohit);

            // Narrate.
            Message.NarrateHitCheck(checkHitResult, target.protection);

            // Calculate damage based on accuracy, precision, and amplitude.
            CalculateStandardDamage(ref result, checkHitResult);

            return result;
        }


        public PromptResult ChooseEnemyTarget()
        {
            // List targets.
            PrintEnemyTargets();

            // Get name. 
            string name;
            Input.PromptTargetName(out name);

            // Set target.
            PromptResult result = TryGetCombatantByName(name, out target);

            return result;
        }

        public PromptResult ChooseEnemyTargetAtRandom()
        {
            List<Combatant> combatants = Combatant.AllByDifferentTeam(owner.team);
            int index = Counting.random.Next(combatants.Count);
            target = combatants[index];

            return PromptResult.Succeeded;
        }

        public PromptResult ChooseFriendlyTargetAtRandom()
        {
            List<Combatant> combatants = Combatant.AllBySameTeam(owner.team);
            int index = Counting.random.Next(combatants.Count);
            target = combatants[index];

            return PromptResult.Succeeded;
        }

        public PromptResult ChooseFriendlyTarget()
        {
            PromptResult promptResult = PromptResult.Succeeded;

            // Display same team.
            PrintOwnTeamTargets();

            // User picks someone from their own team.
            string chosenTarget = "";
            Input.PromptTargetName(out chosenTarget);
            promptResult = TryGetCombatantByName(chosenTarget, out target);

            return promptResult;
        }

        public PromptResult TargetSelf()
        {
            PromptResult result = PromptResult.Succeeded;

            target = owner;

            return result;
        }


        protected void CalculateStandardDamage(ref AbilityResult result, CheckHitResult checkHit, bool canCrit = true)
        {
            if (checkHit.didHit)
            {
                // Check if can crit, where if not, null crit damage.
                if (!canCrit)
                {
                    checkHit.wasCritical = false;
                }

                // Calculate damage.
                int finalDamage =
                    CombatLogic.CalculateEffectiveness(owner.melee.amplitude, attribute.amplitude,
                        checkHit.wasCritical);

                // Inflict damage.
                target.TakeDamage(finalDamage, checkHit.wasBypass);

                // Write to result.
                Message.NarrateHitSuccessDegree(checkHit, result, finalDamage);
                result.didSucceed = checkHit.didHit;
            }
            else
            {
                Message.NarrateMiss();
            }
        }
    }
}