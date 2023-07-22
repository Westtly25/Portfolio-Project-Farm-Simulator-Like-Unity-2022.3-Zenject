using Assets.Scripts.Architecture.State_System;
using Assets.Scripts.Pause_System;

namespace Assets.Scripts.Architecture.State_System
{
    public class PauseState : State
    {
        private readonly IPauseHandler pauseHandler;

        public PauseState(IPauseHandler pauseHandler)
        {
            this.pauseHandler = pauseHandler;
        }

        public override void OnEnter()
        {
            base.OnEnter();

            pauseHandler.SetPaused(true);
        }

        public override void OnExit()
        {
            base.OnExit();

            pauseHandler.SetPaused(false);
        }
    }
}