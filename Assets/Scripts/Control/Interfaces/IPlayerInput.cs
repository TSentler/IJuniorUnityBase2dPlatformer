using UnityEngine;
using UnityEngine.Events;

namespace Control
{
    public interface IPlayerInput
    {
        Vector2 MovementDirection { get; }
        event UnityAction JumpStarted;
    }
}