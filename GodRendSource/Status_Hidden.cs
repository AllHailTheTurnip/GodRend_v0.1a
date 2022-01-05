namespace GodRendSource
{
    public class Status_Hidden :Status
    {
        public int originalDodge;

        public int modifiedDodge;

        public Status_Hidden(Combatant subject, float ability_amplitude, int duration) : base("Smoke Cloud", subject, duration)
        {
            // Increases your dodge; the higher your support:amplitude, the better.
            originalDodge = subject.protection.dodge;

            modifiedDodge = (int) (Counting.Percentile(subject.support.amplitude) * ability_amplitude * originalDodge);
        }

        public override void ApplyEffect()
        {
            Message.Narrate(subject.name + " is enshrouded in a smoke cloud. Dodge: " + modifiedDodge);
            subject.protection.dodge = modifiedDodge;
        }

        public override void RemoveEffect()
        {
            Message.Narrate(subject.name + "'s smoke cloud has disappeared. Dodge: " + originalDodge);
            subject.protection.dodge = originalDodge;
        }
    }
}