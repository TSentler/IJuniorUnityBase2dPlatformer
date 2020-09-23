using Control;
using UnityEngine;

namespace Player
{
    public interface IMovement
    {
        bool IsGrounded { get; }
        bool IsMove { get; }
        Vector3 Position { get; }
        Vector2 Velocity { get; }


        void SetHorizontalVelocity(float x);
        void SetVerticalVelocity(float y);
        void AddHorizontalVelocity(float x);
        void AddVerticalVelocity(float y);
    }
}