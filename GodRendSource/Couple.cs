using Microsoft.VisualBasic.CompilerServices;

namespace GodRendSource
{
    public class Couple<A>
    {
        public A a;
        public A b;

        public Couple(A a, A b)
        {
            this.a = a;
            this.b = b;
        }
        
        public Couple(A a)
        {
            this.a = a;
            this.b = a;
        }

    }
}