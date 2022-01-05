namespace GodRendSource
{
    public class Stamina : Couple<int>
    {
        public Stamina(int @base):base(@base, @base)
        {
            
        }

        public static Stamina operator *(Stamina a, float b)
        {
            int val = (int) (a.a * b);
            return new Stamina(val);
        }
    }
}