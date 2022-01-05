using System;

namespace GodRendSource
{
    public static class Counting
    {
        public static Random random = new Random();

        public enum Comparison
        {
            GreaterThan,
            LessThan
        }

        public static void IncrementWithRollover(ref int item, int max, int min = 0)
        {
            item++;

            if (item > max)
                item = min;

            if (item < min)
                item = max;
        }

        public static void IncrementWithCeiling(ref int value, int amount, int ceiling)
        {
            value += amount;

            if (value > ceiling)
                value = ceiling;
        }

        public static void DecrementWithFloor(ref int value, int amount, int floor)
        {
            value -= amount;

            if (value < floor)
                value = floor;
        }

        public static int Invert(int value, int ceiling = 100)
        {
            return ceiling - value;
        }

        public static float Invert(float value, float ceiling = 1)
        {
            return ceiling - value;
        }

        public static float Percentile(int value)
        {
            return value / 100f;
        }

        public static void AdjustByPercentile(ref int score, int percentile)
        {
            score = (int) (score * Percentile(percentile));
        }

        public static void AdjustByPercentile(ref int score, float percent)
        {
            score = (int) (score * percent);
        }

        public static bool AccuracyCheck(int amount, int low = 5)
        {
            int check = ExplodingDie("Accuracy", amount);
            if (check <= low)
            {
                return true;
            }

            return false;
        }

        public static bool PrecisionCheck(int amount, int checkThreshold = 95)
        {
            int dropCheck = ExplodingDie("Precision", amount);
            if (dropCheck >= checkThreshold)
            {
                return true;
            }

            return false;
        }


        public static int ExplodingDie(string checkType, int range, float percentile = 0.95f)
        {
            int output = 0;

            while (true)
            {
                // 75-125, average = 100.
                int check = random.Next(range) + 1;
                Message.Narrate(checkType + " check (1-" + range + ") = " + check);
                output += check;

                int threshold = (int) (percentile * range);

                if (check >= threshold)
                {
                    Message.Narrate("A die explodes!");
                    continue;
                }
                else
                {
                    break;
                }
            }

            return output;
        }

        public static void MultiplyFlt(ref int value, float coefficient)
        {
            value = (int) (value * coefficient);
        }

        public static int MultiplyFlt(int value, float coefficient)
        {
            return (int) (value * coefficient);
        }
    }
}