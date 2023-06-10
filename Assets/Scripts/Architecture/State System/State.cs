namespace Assets.Scripts.Architecture.State_System
{
    public abstract class State
    {
        protected readonly StateMachine stateMachine;

        public bool IsActive { get; set; }

        public State()
        {

        }

        public State(StateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }

        public virtual void OnEnter() { }
        public virtual void OnRun() { }
        public virtual void OnExit() { }
    }
}
