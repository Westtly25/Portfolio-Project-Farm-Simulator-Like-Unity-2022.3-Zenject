using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Input_System
{
    public interface IInputService
    {
        Vector2 MovementDirection { get; }
    }

    public class InputService
    {
        public Vector2 MovementDirection { get; private set; }
    }
}
