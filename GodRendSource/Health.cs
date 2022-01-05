namespace GodRendSource
{
    public class Health : Couple<int>
    {
        public Health(int @base) : base(@base, @base)
        {
        }


        public override string ToString()
        {
            return "(" + a + "/" + b + ")";
        }

        public static Health operator *(Health a, float b)
        {
            return new Health((int)(a.a * b));
        }
    }
}