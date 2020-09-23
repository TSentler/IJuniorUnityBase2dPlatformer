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
    public class HorizontalMovement : MonoBehaviour
    {
        [SerializeField] private float _speed = 10f;

        private IPlayerInput _playerInput;
        private IMovement _movement;
        private int _lastSignHorizontalMovement;

        private void Awake()
        {
            _lastSignHorizontalMovement = (int)Mathf.Sign(transform.localScale.x);
            _playerInput = GetComponent<IPlayerInput>();
            _movement = GetComponent<IMovement>();
        }

        private void Update()
        {
            float horizontalInput = _playerInput.MovementDirection.x;
            Move(horizontalInput);
            SetSpriteHorizontalDirection((int)Mathf.Sign(horizontalInput));
        }

        private void Move(float horizontalInput)
        {
            var horizontalStep = horizontalInput * _speed;
            _movement.SetHorizontalVelocity(horizontalStep);
        }

        private void SetSpriteHorizontalDirection(int signHorizontalMovement)
        {
            if (_movement.IsMove && _lastSignHorizontalMovement * signHorizontalMovement < 0)
            {
                transform.localScale = new Vector3(
                    Mathf.Abs(transform.localScale.x) * signHorizontalMovement, 
                    transform.localScale.y, 
                    transform.localScale.z);

                _lastSignHorizontalMovement = signHorizontalMovement;
            }
        }
    }
}
