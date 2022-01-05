using System;

namespace GodRendSource
{
    public class Ability_UseItem : Ability
    {
        public Ability_UseItem(Combatant owner) : base(owner, "Use Item")
        {
        }

        public override AbilityResult Execute()
        {
            AbilityResult result = AbilityResult.Empty;
            
            

            // Get user to choose item.
            
            Item foundItem = null;
            while (foundItem == null)
            {
                // If owner is on robot-mode, choose target at random.
                if (owner.mode == Combatant.Mode.Computer)
                {
                    int inventorySize = owner.items.Count;
                    int index = Counting.random.Next(inventorySize);
                    foundItem = owner.items[index];
                    break;
                }
                
                Message.Narrate("Please choose an item to use on " + target.name);
                
                // List available items.
                foreach (var item in owner.items)
                {
                    Message.Narrate(" - " + item.name);
                }

                // Get item name.
                string wantedItem = Input.PromptString("Choose an item.");

                // Find item in list.
                foreach (Item item in owner.items)
                {
                    if (item.name == wantedItem)
                    {
                        foundItem = item;
                        result.didSucceed = true;
                        break;
                    }
                }

                // Repeat until item is found.
            }

            if(owner == target)
            {
                Message.Narrate(owner.name + " uses " + foundItem.name + ".");
            }
            else
            {
                Message.Narrate(owner.name + " uses on " + foundItem.name + " on " + target.name);
            }
            
            // Execute item's function with the user as parameter.
            foundItem.ApplyToTarget(target);
            owner.items.Remove(foundItem);

            return result;
        }

        public override PromptResult ChooseTarget(Combatant.Mode mode)
        {
            PromptResult promptResult = PromptResult.Succeeded;

            if (mode == Combatant.Mode.Computer)
                return ChooseFriendlyTargetAtRandom();

            // Display same team.
            PrintOwnTeamTargets();

            // User picks someone from their own team.
            string chosenTarget = "";
            Input.PromptTargetName(out chosenTarget);
            promptResult = TryGetCombatantByName(chosenTarget, out target);

            return promptResult;
        }

        public override bool CanUse()
        {
            return owner.items.Count > 0;
        }
    }
}