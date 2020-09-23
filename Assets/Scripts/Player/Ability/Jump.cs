using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Control;

namespace Player.Ability
{
    [RequireComponent(typeof(IPlayerInput))]
    [RequireComponent(typeof(IMovement))]
    public class Jump : MonoBehaviour
    {
        [SerializeField] private float _power = 10f;

        private IPlayerInput _playerInput;
        private IMovement _movement;

        private void Awake()
        {
            _playerInput = GetComponent<IPlayerInput>();
            _movement = GetComponent<IMovement>();
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
            if (_movement.IsGrounded)
                _movement.SetVerticalVelocity(_power);
        }
    }
}
