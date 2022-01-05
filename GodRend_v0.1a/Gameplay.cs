using System;
using System.Collections.Generic;
using GodRendSource;
using static System.Console;

namespace GodRend_v0._1a
{
    class Gameplay
    {
        /*
         * Variables.
         */
        private int turnIndex = 0;

        /*
         * Static instances.
         */
        public static Random random = Counting.random;

        /*
         * Instances.
         */
        private Combatant offender;

        /*
         * Static methods.
         */
        static void Main(string[] args)
        {
            Gameplay gameplay;
            gameplay = new Gameplay();
            gameplay.LaunchCombatLoop();
        }

        /*
         * Methods.
         */

        void SearchAndReadyAction(Combatant.Mode mode)
        {
            if (mode == Combatant.Mode.Computer)
            {
                // Choose at random.
                offender.ChooseAbilityAtRandom();
            }
            else if (mode == Combatant.Mode.Player)
            {
                while (true)
                {
                    // List combatant actions.
                    List<String> listOfActions = offender.GetUsableAbilities();
                    Message.Narrate(listOfActions);

                    // Prompt user for action by name.
                    string abilityName = Input.PromptString("Please choose an action.");

                    // Attempt to ready the ability; otherwise, return null.
                    bool foundAndReadiedAbility = offender.ReadyAbilityByName(abilityName);

                    // If ability wasn't found by the entered name, it'll complain.
                    if (!foundAndReadiedAbility)
                    {
                        Message.ErrorAnyKey(
                            "Could not find ability by name of '" + abilityName + "', please try again.");
                        continue;
                    }

                    break;
                }
            }
        }

        Combatant GetDefendingEnemyCombatant(ExecutionMode mode)
        {
            if (mode == ExecutionMode.auto)
            {
                // Get first enemy combatant from index.
                for (int i = 0; i < Combatant.Total; i++)
                {
                    Combatant combatant = Combatant.GetByIndex(i);
                    // Check if enemy.
                    if (offender.team != combatant.team)
                        return combatant;
                }
            }

            throw new Exception("Failed to get subject combatant.");
        }

        bool CheckIfGameIsOver()
        {
            if (Combatant.Total == 0)
            {
                Message.Narrate("Battle has ended. Everyone is dead.");
                return true;
            }
            // Check if team 0 is all dead.
            else if (Combatant.TotalByTeam(0) == 0)
            {
                Message.Narrate("Team 0 has been defeated.");
                return true;
            }
            // Check if team 1 is all dead.
            else if (Combatant.TotalByTeam(1) == 0)
            {
                Message.Narrate("Team 1 has been defeated.");
                return true;
            }

            return false;
        }

        void LaunchCombatLoop()
        {
            // Present list of combatants.
            /*
             *  Demo1       Demo4
             *  Demo2   VS  Demo5
             *  Demo3       Demo6
             */
            for (int i = 0; i < Combatant.Total; i++)
            {
                Combatant combatant = Combatant.GetByIndex(i);
                Message.Narrate(combatant.name + ", Team " + combatant.team);
            }


            // General loop.
            while (true)
            {
                // Set offending combatant.
                try
                {
                    offender = Combatant.GetByIndex(turnIndex);
                }
                catch
                {
                    offender = Combatant.GetByIndex(turnIndex-1);
                }

                

                // Announce turn.
                Message.Narrate("It is " + offender.NameAndStatus + "'s turn.");

                // Inflict any status effects.
                CombatLogic.UpdateStatusEffects(offender);

                // ... Skip if dead. 
                if (offender.IsAlive == false)
                {
                    Counting.IncrementWithRollover(ref turnIndex, Combatant.Total);
                    continue;
                }

                if (CheckIfGameIsOver())
                    break;

                // Choose offending combatant's ability.
                SearchAndReadyAction(offender.mode);

                // Set target.
                while (true)
                {
                    PromptResult result = offender.SetAbilityTarget(offender.mode);
                    if (result.succeeded == false)
                    {
                        Message.Narrate(result.reason);
                        continue;
                    }

                    break;
                }


                // Execute action.
                offender.ExecuteReadiedAbility();


                // Check if everyone has been remove from the list. If so, break the loop.
                if (CheckIfGameIsOver())
                    break;


                // Increase turn index, allowing next combatant to take turn.
                Counting.IncrementWithRollover(ref turnIndex, Combatant.Total - 1);
            }
        }

        public Gameplay()
        {
            // Create combatants.
            Combatant_Manslayer manslayer = new Combatant_Manslayer(0, Combatant.Mode.Computer);
            Combatant_Chimera chimera = new Combatant_Chimera(0, Combatant.Mode.Computer);
            Combatant_Raiden raiden = new Combatant_Raiden(0, Combatant.Mode.Computer);
            Combatant_Architect architect = new Combatant_Architect(0, Combatant.Mode.Computer);
            Combatant_Accutron accutron = new Combatant_Accutron(0, Combatant.Mode.Computer);
            Combatant_Xoltan xoltan = new Combatant_Xoltan(0, Combatant.Mode.Computer);
            
            // Scramble the combatants
            Combatant.ScrambleAll();
            
            // Make the first three combatants controlled by Player.
            for (int i = 0; i < 3; i++)
            {
                Combatant.GetByIndex(i).mode = Combatant.Mode.Player;
                Combatant.GetByIndex(i).team = 1;
            }

            /*  DemoCombatant demo2 = new DemoCombatant("demo2", 0);
               DemoCombatant demo3 = new DemoCombatant("demo3", 0);
               DemoCombatant demo4 = new DemoCombatant("demo4", 1);
               DemoCombatant demo5 = new DemoCombatant("demo5", 1);
               DemoCombatant demo6 = new DemoCombatant("demo6", 1);*/

            /*
             * Xoltan - Sorcerer (Finished)
             * Accutron - Archer (Finished)
             * Raiden - Ninja (Finished)
             * Chimera - Pugilist (Finished)
             * Architect - Healer (Finished)
             * Manslayer - Swordsman
             */
        }
    }
}