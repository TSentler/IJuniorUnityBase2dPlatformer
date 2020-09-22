namespace Player.Ability
{
    internal interface IPhysicsMovement
    {
        void SetHorizontalVelocity(float x);
        void SetVerticalVelocity(float y);
        void AddHorizontalVelocity(float x);
        void AddVerticalVelocity(float y);
    }
}