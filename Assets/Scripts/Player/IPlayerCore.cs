using Control;
using UnityEngine;

namespace Player
{
    public interface IPlayerCore
    {
        bool IsInitialized { get; }
        GameObject GameObject { get; }
        Transform Transform { get; }
        IInputHandler InputHandler { get; }
        Rigidbody2D Rigidbody { get; }
        Animator Animator { get; }
    }
}