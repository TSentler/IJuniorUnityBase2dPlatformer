using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Control;

namespace Player.Ability
{
    [RequireComponent(typeof(IPlayerInput))]
    [RequireComponent(typeof(IMovement))]
    [RequireComponent(typeof(IGroundable))]
    [RequireComponent(typeof(Animator))]
    public class Jump : MonoBehaviour
    {
        [SerializeField] private float _power = 10f;

        private IPlayerInput _playerInput;
        private IGroundable _ground;
        private IMovement _movement;
        private Animator _animator;
        private bool _lastAirState;

        private void Awake()
        {
            _playerInput = GetComponent<IPlayerInput>();
            _ground = GetComponent<IGroundable>();
            _movement = GetComponent<IMovement>();
            _animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            _playerInput.JumpStarted += JumpStarted;
        }

        private void OnDisable()
        {
            _playerInput.JumpStarted -= JumpStarted;
        }

        private void JumpStarted()
        {
            if (_ground.IsGrounded)
                _movement.SetVerticalVelocity(_power);
        }

        private void Update()
        {
            SetAirAnimation(_ground.IsGrounded == false);
        }

        private void SetAirAnimation(bool inAir)
        {
            if (_lastAirState != inAir)
            {
                _animator.SetBool(AnimatorKeys.InAir, inAir);
                _animator.SetTrigger(AnimatorKeys.Jumped);
                _lastAirState = inAir;
            }
        }
    }
}
