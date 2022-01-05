using System.Collections.Generic;

namespace GodRendSource
{
    public class DemoCombatant : Combatant
    {
        public DemoCombatant(string name, int team, Mode mode) : base(name,
            new Health(100),
            new Stamina(100),
            new AttributeI(100, 100, 100),
            new AttributeI(100, 100, 100),
            new AttributeI(100, 100, 100),
            new Protection(50, 85, 100), team, mode)
        {
            GrantStandardAbilities(this);
            GrantStandardItems(this);
        }
    }
}