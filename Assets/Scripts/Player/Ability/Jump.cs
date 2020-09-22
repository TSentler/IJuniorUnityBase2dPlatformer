using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tools;

namespace Player.Ability
{
    [RequireComponent(typeof(IPhysicsMovement), typeof(IGroundable))]
    public class Jump : Ability
    {
        [SerializeField] private float _power = 10f;
        [SerializeField] private IGroundable _ground;
        [SerializeField] private IPhysicsMovement _physicsMovement;

        private bool _lastAirState;

        private void OnValidate()
        {
            _ground.VerifyNotNull<IGroundable>(nameof(_ground));
            _physicsMovement.VerifyNotNull<IPhysicsMovement>(nameof(_physicsMovement));
        }

        private void OnEnable()
        {
            if (Core.InputHandler != null)
                Core.InputHandler.JumpStarted += JumpStarted;
        }

        private void OnDisable()
        {
            if (Core.InputHandler != null)
                Core.InputHandler.JumpStarted -= JumpStarted;
        }

        private void JumpStarted()
        {
            if (_ground.IsGrounded)
                _physicsMovement.SetVerticalVelocity(_power);
        }

        private void Update()
        {
            SetAirAnimation(_ground.IsGrounded == false);
        }

        private void SetAirAnimation(bool inAir)
        {
            if (_lastAirState != inAir)
            {
                Core.Animator.SetBool(AnimatorKeys.InAir, inAir);
                Core.Animator.SetTrigger(AnimatorKeys.Jumped);
                _lastAirState = inAir;
            }
        }

    }
}
