namespace GodRendSource
{
    public class Attribute<T>
    {
        public T amplitude, accuracy, precision;
        public Attribute(T amplitude, T accuracy, T precision) {
            // Amplitude = power!
            this.amplitude = amplitude;
            // Accuracy = how likely it is to succeed/hit.
            this.accuracy = accuracy;
            // Precision = likeliness of best results.
            this.precision = precision;
        }
        
        
    }

    public class AttributeI : Attribute<int>
    {
        public AttributeI(int amplitude = 100, int accuracy = 100, int precision = 100) : base(amplitude, accuracy, precision)
        {
            
        }

        public static AttributeI operator *(AttributeI a, float b)
        {
            return new AttributeI((int) (a.amplitude * b), (int) (a.accuracy * b), (int) (a.precision * b));
        }
    }

    public class AttributeF : Attribute<float>
    {
        public AttributeF(float amplitude = 1.0f, float accuracy = 1.0f, float precision = 1.0f) : base(amplitude, accuracy, precision)
        {
        }

        public static AttributeF operator *(AttributeF a, float b)
        {
            return new AttributeF(a.amplitude * b, a.accuracy * b, a.precision * b);
        }
    }
}