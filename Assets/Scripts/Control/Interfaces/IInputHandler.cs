using UnityEngine;
using UnityEngine.Events;

namespace Control
{
    public interface IInputHandler
    {
        Vector2 MovementDirection { get; }
        event UnityAction JumpStarted;
    }
}