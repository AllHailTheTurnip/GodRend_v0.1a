namespace GodRendSource
{
    public class Status_Freeze : Status
    {
        public int originalDodge;

        public int AfflictedDodge => originalDodge / 2;
        
        public Status_Freeze(Combatant subject, int duration) : base("Freeze", subject, duration)
        {
            originalDodge = subject.protection.dodge;
            
            ApplyEffect();
        }
        
        public override void ApplyEffect()
        {
            subject.protection.dodge = AfflictedDodge;
        }

        public override void RemoveEffect()
        {
            subject.protection.dodge = originalDodge;
        }
    }
}