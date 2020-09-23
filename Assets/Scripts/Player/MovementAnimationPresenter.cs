using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(IMovement))]
    [RequireComponent(typeof(Animator))]
    public class MovementAnimationPresenter : MonoBehaviour
    {
        private IMovement _movement;
        private Animator _animator;
        private bool _lastAirState;

        private void Awake()
        {
            _movement = GetComponent<IMovement>();
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            SetAirAnimation();
            SetRunAnimation();
        }

        private void SetAirAnimation()
        {
            var inAir = _movement.IsGrounded == false;
            if (_lastAirState != inAir)
            {
                _animator.SetBool(AnimatorKeys.InAir, inAir);
                _animator.SetTrigger(AnimatorKeys.Jumped);
                _lastAirState = inAir;
            }
        }

        private void SetRunAnimation()
        {
            var isRun = _movement.IsMove && _movement.IsGrounded;
            _animator.SetBool(AnimatorKeys.IsRun, isRun);
        }
    }
}
