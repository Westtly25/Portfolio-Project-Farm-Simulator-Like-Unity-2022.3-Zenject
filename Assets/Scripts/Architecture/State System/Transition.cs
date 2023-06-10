using System;

namespace Assets.Scripts.Architecture.State_System
{
    public class Transition
    {
        public Func<bool> Condition { get; }
        public State From { get; }
        public State To { get; }

        public Transition(State from, State to, Func<bool> condition)
        {
            From = from;
            To = to;
            Condition = condition;
        }
    }
}
