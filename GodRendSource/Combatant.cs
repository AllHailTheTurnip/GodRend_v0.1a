using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;

namespace GodRendSource
{
    public class Combatant
    {
        public static Combatant GetByName(string name)
        {
            // Insensitive to case; caps or no caps, doesn't matter.
            foreach (Combatant combatant in all)
            {
                string COMBATANTNAME = combatant.name.ToUpper();
                string NAME = name.ToUpper();

                if (COMBATANTNAME == NAME)
                {
                    return combatant;
                }
            }

            return null;
        }

        public Ability GetAbilityByName(string enteredName)
        {
            Ability @return = null;

            foreach (var ability in abilities)
            {
                if (ability.name == enteredName)
                {
                    @return = ability;
                    break;
                }
            }

            return @return;
        }

        public static string[] AllAsList()
        {
            string[] @return = new String [Total];

            for (int i = 0; i < Total; i++)
            {
                @return[i] = all[i].ToString();
            }

            return @return;
        }

        public static void ScrambleAll()
        {
            List<Combatant> newList = new List<Combatant>();
            int listLength = all.Count;
            for (int i = 0; i < listLength; i++)
            {
                int randomIndex = Counting.random.Next(all.Count);
                newList.Add(all[randomIndex]);
                all.Remove(GetByIndex(randomIndex));
            }

            all = newList;
        }

        public static void ChangeCombatantMode(Combatant combatant, Mode mode)
        {
            combatant.mode = mode;
        }

        public static List<Combatant> AllByTeam(int team)
        {
            // Count the amount of matching-team members.
            List<Combatant> combatants = new List<Combatant>();

            foreach (var combatant in Combatant.all)
            {
                if (combatant.team == team)
                {
                    combatants.Add(combatant);
                }
            }

            return combatants;
        }

        public static int TotalByTeam(int team)
        {
            int total = 0;

            foreach (var combatant in all)
            {
                if (combatant.team == team)
                    total++;
            }

            return total;
        }

        public static List<Combatant> AllByDifferentTeam(int team)
        {
            // Count the amount of matching-team members.
            List<Combatant> combatants = new List<Combatant>();

            foreach (var combatant in all)
            {
                if (combatant.team != team)
                {
                    combatants.Add(combatant);
                }
            }

            return combatants;
        }

        public static List<Combatant> AllBySameTeam(int team)
        {
            // Count the amount of matching-team members.
            List<Combatant> combatants = new List<Combatant>();

            foreach (var combatant in all)
            {
                if (combatant.team == team)
                {
                    combatants.Add(combatant);
                }
            }

            return combatants;
        }

        public AbilityResult ExecuteReadiedAbility()
        {
            stamina.a -= readiedAbility.staminaCost;
            return readiedAbility.Execute();
        }

        public bool ReadyAbilityByName(String abilityName)
        {
            Ability ability = GetAbilityByName(abilityName);

            if (ability == null || ability.CanUse() == false)
            {
                // Failed to find ability.
                return false;
            }

            readiedAbility = ability;

            // Success.
            return true;
        }

        public bool ReadyAbilityByIndex(int index)
        {
            readiedAbility = abilities[index];
            return true;
        }

        public bool ChooseAbilityAtRandom()
        {
            List<Ability> availableAbilities = new List<Ability>();
            // Gather abilities based on availability.
            foreach (Ability ability in abilities)
            {
                if(ability.CanUse())
                    availableAbilities.Add(ability);
            }

            // Choose one at random.
            int randomIndex = Counting.random.Next(availableAbilities.Count);
            readiedAbility = availableAbilities[randomIndex];
            
            return true;
        }


        public Combatant(string name,
            Health health,
            Stamina stamina,
            AttributeI melee,
            AttributeI ranged,
            AttributeI support,
            Protection protection, 
            int team,
            Mode mode)
        {
            foreach (Combatant combatant in all)
            {
                if (combatant.name == name)
                {
                    // Copy of name found; amend new name.
                    name += '!';
                }
            }
            
            
            this.name = name;
            this.health = health;
            this.stamina = stamina;
            this.melee = melee;
            this.ranged = ranged;
            this.support = support;
            this.protection = protection;
            this.team = team;
            this.mode = mode;
            abilities = new List<Ability>();
            
            all.Add(this);
        }

        public void TakeDamage(int amount, bool wasBypass)
        {
            if (wasBypass)
            {
                health.a -= amount;
            }
            else
            {
                // Apply to attrition.
                protection.attrition -= amount;

                if (protection.attrition < 0)
                {
                    health.a -= -(protection.attrition);
                }
            }

            // Check if dead.
            if (health.a <= 0)
            {
                // You are dead!
                Message.Narrate(name + " has been slain!");

                // Remove from all.
                all.Remove(this);
            }
        }

        public void RemoveAllStatusEffects()
        {
            statusEffects.Clear();
        }

        public void DamageOnlyArmorAttrition(int amount)
        {
            protection.attrition -= amount;
            
            if (amount < 0)
                protection.attrition = 0;
        }

        public void InflictStatusFreeze(int turnDuration)
        {
            Status_Freeze freeze = new Status_Freeze(this, turnDuration);

            statusEffects.Add(freeze);
        }

        public void InflictStatusHidden(int turnDuration, float ability_amplitude)
        {
            Status_Hidden hidden = new Status_Hidden(this, ability_amplitude, turnDuration);
            
            statusEffects.Add(hidden);
            
            hidden.ApplyEffect();
        }

        public void InflictStatusDoom(int timeRemaining)
        {
            Status_Doom doom = new Status_Doom(this, timeRemaining);
            statusEffects.Add(doom);
        }

        public void RestoreHealth(int amount)
        {
            health.a += amount;

            if (health.a > health.b)
                health.a = health.b;
        }

        public void IncreaseArmorAttrition(int amount)
        {
            protection.IncreaseAttrition(amount);
        }

        public void IncreaseArmorCover(int amount)
        {
            protection.IncreaseCover(amount);
        }

        public void IncreaseAttributeAspect(ref int value, int amount)
        {
            Counting.IncrementWithCeiling(ref value, amount, 200);
        }

        public void DecreaseAttributeAspect(ref int value, int amount)
        {
            Counting.DecrementWithFloor(ref value, amount, 50);
        }

        public static void GrantStandardAbilities(Combatant combatant)
        {
            Ability_Punch punch = new Ability_Punch(combatant);
            Ability_ThrowStone throwStone = new Ability_ThrowStone(combatant);
            Ability_UseItem useItem = new Ability_UseItem(combatant);
            List<Ability> abilities = new List<Ability>()
            {
                punch,
                throwStone,
                useItem
            };
            combatant.abilities.AddRange(abilities);
        }

        public static void GrantStandardItems(Combatant combatant)
        {
            // 3x Health potions.
            GrantHealthPotion(combatant);
            GrantHealthPotion(combatant);
            GrantHealthPotion(combatant);

            // 2x Adrenaline potions.
            combatant.items.Add(new Item_AdrenalinePotion());
            combatant.items.Add(new Item_AdrenalinePotion());

            // 2x Focus potions.
            combatant.items.Add(new Item_FocusPotion());
            combatant.items.Add(new Item_FocusPotion());
        }

        public static void GrantHealthPotion(Combatant combatant)
        {
            combatant.items.Add(new Item_HealthPotion());
        }

        public override string ToString()
        {
            // Name, its health
            return name + "(" + health.a + "/" + health.b + ")";
        }

        public PromptResult SetAbilityTarget(Mode mode)
        {
            return readiedAbility.ChooseTarget(mode);
        }

        public PromptResult SetAbilityTarget(Combatant target, Mode mode)
        {
            readiedAbility.target = target;
            return PromptResult.Succeeded;
        }

        public static Combatant GetByIndex(int index)
        {
            return all[index];
        }

        private static List<Combatant> all = new List<Combatant>();

        public enum Mode
        {
            Player,
            Computer
        }

        public static int Total
        {
            get { return all.Count; }
        }

        public Mode mode;

        // Attributes.
        public const int ATTB_ULTRA_LOW = 25;
        public const int ATTB_VERY_LOW = 50;
        public const int ATTB_LOW = 75;
        public const int ATTB_AVERAGE = 100;
        public const int ATTB_HIGH = 125;
        public const int ATTB_VERY_HIGH = 150;
        public const int ATTB_ULTRA_HIGH = 175;
        
        // Vitals.
        public const int VITAL_VERY_LOW = 75;
        public const int VITAL_LOW = 100;
        public const int VITAL_AVERAGE = 150;
        public const int VITAL_HIGH = 200;
        public const int VITAL_VERY_HIGH = 250;
        
        // Dodge.
        public const int DODGE_VERY_LOW = 5;
        public const int DODGE_LOW = 10;
        public const int DODGE_AVERAGE = 15;
        public const int DODGE_HIGH = 20;
        public const int DODGE_VERY_HIGH = 25;
        
        // Cover.
        public const int COVER_VERY_LOW = 20;
        public const int COVER_LOW = 40;
        public const int COVER_AVERAGE = 85;
        public const int COVER_HIGH = 125;
        public const int COVER_VERY_HIGH = 185;
        
        // Attrition.
        public const int ATTR_VERY_LOW = 25;
        public const int ATTR_LOW = 50;
        public const int ATTR_AVERAGE = 100;
        public const int ATTR_HIGH = 150;
        public const int ATTR_VERY_HIGH = 200;
        

        public List<Item> items = new List<Item>();

        public int team { get; set; }
        public string name { get; protected set; }
        

        public bool IsAlive
        {
            get { return health.a > 0; }
        }

        public List<Status> statusEffects = new List<Status>();

        public List<String> GetUsableAbilities()
        {
            List<String> result = new List<string>();

            foreach (Ability ability in abilities)
            {
                if (ability.CanUse())
                    result.Add(ability.FormalName());
            }

            return result;
        }

        public string NameAndStatus
        {
            get
            {
                // Create the base.
                string status = name;

                // List health.
                if (!IsAlive)
                    status += "(DEAD";
                else
                    status += "(HP:" + health.a + "/" + health.b + "; SP:" + stamina.a + "/" + stamina.b;

                // List armor attrition.
                if (protection.attrition > 0)
                    status += "{Armor:" + protection.attrition + "}";

                // List status effects.
                if (statusEffects.Count > 0)
                {
                    status += "[";

                    foreach (Status effect in statusEffects)
                    {
                        status += effect.name + " ";
                    }

                    status += "]";
                }

                // Put on the cap.
                status += ")";

                return status;
            }
        }

        public Health health { get; protected set; }

        public Stamina stamina { get; protected set; }

        public Couple<int> specialAmmunition { get; protected set; }

        public AttributeI melee { get; protected set; }
        public AttributeI ranged { get; protected set; }
        public AttributeI support { get; protected set; }

        public Protection protection { get; protected set; } // a = dodge, b = cover, c = attrition.

        public List<Ability> abilities { get; protected set; }
        public Ability readiedAbility;

        public Combatant AbilityTarget
        {
            get { return readiedAbility.target; }
            set { readiedAbility.target = value; }
        }
    }
}