using System;
using System.Collections.Generic;

namespace Assets.Scripts.Architecture.State_System
{
    public class StateMachine
    {
        public bool TransitionsEnabled { get; set; } = true;
        public bool HasCurrentState { get; private set; }

        public State CurrentState { get; private set; }
        public Transition CurrentTransition { get; private set; }

        private readonly HashSet<State> states = new(6);
        private readonly List<Transition> anyTransitions = new(6);
        private readonly List<Transition> transitions = new(6);

        private bool _isStatesAdded;

        public StateMachine() { }

        public void AddStates(params State[] states)
        {
            if (_isStatesAdded)
            {
                throw new Exception("States already added!");
            }

            foreach (var state in states)
            {
                if (state == null)
                {
                    throw new NullReferenceException(nameof(state));
                }

                this.states.Add(state);
            }

            if (states.Length > 0)
            {
                _isStatesAdded = true;
            }
        }

        public void SetState<TState>() where TState : State
        {
            SetState(typeof(TState));
        }

        public void AddTransition<TStateFrom, TStateTo>(Func<bool> condition)
            where TStateFrom : State
            where TStateTo : State
        {
            var stateFrom = GetState(typeof(TStateFrom));
            var stateTo = GetState(typeof(TStateTo));

            transitions.Add(new Transition(stateFrom, stateTo, condition));
        }

        public void AddAnyTransition<TStateTo>(Func<bool> condition)
            where TStateTo : State
        {
            var stateTo = GetState(typeof(TStateTo));

            anyTransitions.Add(new Transition(null, stateTo, condition));
        }

        public void Run()
        {
            if (states == null)
                return;

            if (TransitionsEnabled)
                SetStateByTransitions();

            if (HasCurrentState)
                CurrentState.OnRun();
        }

        public void SetStateByTransitions()
        {
            CurrentTransition = GetTransition();

            if (CurrentTransition == null)
                return;

            if (CurrentState == CurrentTransition.To)
                return;

            SetState(CurrentTransition.To.GetType());
        }

        public TState GetState<TState>() where TState : State
        {
            return (TState)GetState(typeof(TState));
        }

        private State GetState(Type type)
        {
            foreach (var state in states)
            {
                if (state.GetType() == type)
                {
                    return state;
                }
            }

            throw new Exception($"The <{type.Name}> is not found!");
        }

        private void SetState(Type type)
        {
            if (HasCurrentState)
            {
                ExitCurrentState();
            }

            CurrentState = GetState(type);
            HasCurrentState = true;

            EnterCurrentState();
        }

        private void EnterCurrentState()
        {
            CurrentState.IsActive = true;
            CurrentState.OnEnter();
        }

        private void ExitCurrentState()
        {
            CurrentState.IsActive = false;
            CurrentState.OnExit();
        }

        private Transition GetTransition()
        {
            for (var i = 0; i < anyTransitions.Count; i++)
            {
                if (anyTransitions[i].Condition())
                {
                    return anyTransitions[i];
                }
            }

            for (var i = 0; i < transitions.Count; i++)
            {
                if (transitions[i].From.IsActive == false)
                {
                    continue;
                }

                if (transitions[i].Condition())
                {
                    return transitions[i];
                }
            }

            return default;
        }
    }
}
