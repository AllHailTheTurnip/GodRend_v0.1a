using System;

namespace GodRendSource
{
    public abstract class Status
    {
        public string name;
        public int duration;
        public Combatant subject;
        public bool IsExpired => duration == 0;

        public virtual void ApplyEffect()
        {
            throw new Exception("Not implemented!");
        }

        public virtual void RemoveEffect()
        {
            throw new Exception("Not implemented!");
        }

        public void ReduceDuration(int amount = 1)
        {
            duration -= amount;
        }

        public Status(string name, Combatant subject, int duration)
        {
            this.subject = subject;
            this.duration = duration;
            this.name = name;
        }
    }
}