using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tools;

namespace Player.Ability
{
    [RequireComponent(typeof(IPhysicsMovement))]
    public class Movement : Ability
    {
        [SerializeField] private float _speed = 10f;
        [SerializeField] private IPhysicsMovement _physicsMovement;

        private int _lastSignHorizontalMovement;

        private bool IsMove => Mathf.Approximately(Core.InputHandler.MovementDirection.x, 0f) == false;

        private void OnValidate()
        {
            _physicsMovement.VerifyNotNull<IPhysicsMovement>(nameof(_physicsMovement));
        }

        protected override void Awake()
        {
            base.Awake();   
            _lastSignHorizontalMovement = (int)Mathf.Sign(transform.localScale.x);
        }

        private void Update()
        {
            float horizontalInput = Core.InputHandler.MovementDirection.x;
            Move(horizontalInput);
            SetSpriteHorizontalDirection((int)Mathf.Sign(horizontalInput));
            SetMoveAnimationActive(IsMove);
        }

        private void Move(float horizontalInput)
        {
            var horizontalStep = horizontalInput * _speed;
            _physicsMovement.SetHorizontalVelocity(horizontalStep);
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
            Core.Animator.SetBool(AnimatorKeys.IsRun, isRun);
        }
    }
}
