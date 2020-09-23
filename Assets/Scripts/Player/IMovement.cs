using Control;
using UnityEngine;

namespace Player
{
    public interface IMovement
    {
        Vector3 Position { get; }
        Vector2 Velocity { get; }

        void SetHorizontalVelocity(float x);
        void SetVerticalVelocity(float y);
        void AddHorizontalVelocity(float x);
        void AddVerticalVelocity(float y);
    }
}