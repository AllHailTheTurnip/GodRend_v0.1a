namespace GodRendSource
{
    public class Protection
    {
        public int dodge, cover, attrition;

        public override string ToString()
        {
            return "Ddg: " + dodge + "(Cvr: " + cover + ", Attr: " + attrition + ")";
        }

        public Protection(int dodge, int cover, int attrition)
        {
            this.dodge = dodge;
            this.cover = cover;
            this.attrition = attrition;
        }

        public void IncreaseAttrition(int amount)
        {
            attrition += amount;
        }

        public void IncreaseCover(int amount)
        {
            cover += amount;

            if (cover > Combatant.COVER_VERY_HIGH)
                cover = Combatant.COVER_VERY_HIGH;
        }
    }
}