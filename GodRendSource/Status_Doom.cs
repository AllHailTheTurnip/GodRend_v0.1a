namespace GodRendSource
{
    public class Status_Doom : Status
    {
        public Status_Doom(Combatant subject, int timeRemaining) : base("Doom", subject, timeRemaining)
        {
        }

        public override void ApplyEffect()
        {
            Message.Narrate(subject.name + " hears a bell tolling somewhere in the distance... It rings " + duration +
                            " times.");
        }

        public override void RemoveEffect()
        {
            // Kills the target.
            Message.Narrate(subject.name + "'s doom has come!");
            subject.TakeDamage(subject.health.a, true);
        }
    }
}