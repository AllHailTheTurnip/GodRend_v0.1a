namespace GodRendSource
{
    public class CheckHitResult
    {
        // Check, Did hit, was critical, was bypass.
        public int check;
        public bool didHit, wasCritical, wasBypass;

        public CheckHitResult(int check, bool didHit, bool wasCritical = false, bool wasBypass = false)
        {
            this.check = check;
            this.didHit = didHit;
            this.wasCritical = wasCritical;
            this.wasBypass = wasBypass;
        }
    }
}