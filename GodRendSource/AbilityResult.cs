namespace GodRendSource
{
    public class AbilityResult
    {
        public int finalAmount = 0;
        public int finalRoll = 0;
        public bool didSucceed = false, wasCritical = false;

        public AbilityResult(string narration, int finalAmount, int finalRoll, bool didSucceed, bool wasCritical)
        {
            this.finalAmount = finalAmount;
            this.finalRoll = finalRoll;
            this.didSucceed = didSucceed;
            this.wasCritical = wasCritical;
        }

        public static AbilityResult Succeeded(string narration, int amount, int finalRoll, bool wasCritical = false)
        {
            return new AbilityResult(narration, amount, finalRoll, true, wasCritical);
        }

        public static AbilityResult Failed(string narration, int finalRoll)
        {
            return new AbilityResult(narration, 0, finalRoll, false, false);
        }

        public static AbilityResult Empty
        {
            get { return new AbilityResult("", 0, 0, false, false); }
        }
    }
}