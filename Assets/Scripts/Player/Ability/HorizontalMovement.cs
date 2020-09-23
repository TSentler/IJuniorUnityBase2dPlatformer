using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tools;
using Control;

namespace Player.Ability
{
    [RequireComponent(typeof(IMovement))]
    [RequireComponent(typeof(IPlayerInput))]
    [RequireComponent(typeof(Animator))]
    public class HorizontalMovement : MonoBehaviour
    {
        [SerializeField] private float _speed = 10f;

        private IPlayerInput _playerInput;
        private IMovement _movement;
        private int _lastSignHorizontalMovement;
        private Animator _animator;

        private bool IsMove => 
            Mathf.Approximately(_movement.Velocity.x, 0f) == false;

        private void Awake()
        {
            _lastSignHorizontalMovement = (int)Mathf.Sign(transform.localScale.x);
            _playerInput = GetComponent<IPlayerInput>();
            _movement = GetComponent<IMovement>();
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            float horizontalInput = _playerInput.MovementDirection.x;
            Move(horizontalInput);
            SetSpriteHorizontalDirection((int)Mathf.Sign(horizontalInput));
            SetMoveAnimationActive(IsMove);
        }

        private void Move(float horizontalInput)
        {
            var horizontalStep = horizontalInput * _speed;
            _movement.SetHorizontalVelocity(horizontalStep);
        }

        private void SetSpriteHorizontalDirection(int signHorizontalMovement)
        {
            if (IsMove && _lastSignHorizontalMovement * signHorizontalMovement < 0)
            {
                transform.localScale = new Vector3(
                    Mathf.Abs(transform.localScale.x) * signHorizontalMovement, 
                    transform.localScale.y, 
                    transform.localScale.z);

                _lastSignHorizontalMovement = signHorizontalMovement;
            }
        }

        private void SetMoveAnimationActive(bool isRun)
        {
            _animator.SetBool(AnimatorKeys.IsRun, isRun);
        }
    }
}
