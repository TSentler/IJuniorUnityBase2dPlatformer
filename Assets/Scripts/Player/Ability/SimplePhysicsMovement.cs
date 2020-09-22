using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Ability
{
    public class SimplePhysicsMovement : Ability, IGroundable, IPhysicsMovement
    {
        private const float _minMoveDistance = 0.001f;

        private readonly RaycastHit2D[] _hitBuffer = new RaycastHit2D[16];

        [SerializeField] private Vector2 _velocity;
        [SerializeField] private Vector2 _velocityBuffer;
        [SerializeField] private float _minGroundNormalY = .65f;
        [SerializeField] private LayerMask _layerMask;

        private ContactFilter2D _contactFilter;

        [ShowInInspector] public bool IsGrounded { get; private set; }

        public Vector2 Velocity => _velocity;

        private void Start()
        {
            _contactFilter.useTriggers = false;
            _contactFilter.SetLayerMask(_layerMask);
            _contactFilter.useLayerMask = true;
        }
        
        public void SetHorizontalVelocity(float x)
        {
            _velocity.x = x;
        }

        public void SetVerticalVelocity(float y)
        {
            _velocity.y = y;
        }
        public void AddHorizontalVelocity(float x)
        {
            _velocity.x += x;
        }

        public void AddVerticalVelocity(float y)
        {
            _velocity.y += y;
        }

        private void FixedUpdate()
        {
            IsGrounded = false;

            int count = Core.Rigidbody.Cast(-Core.Transform.up, _contactFilter, _hitBuffer, _minGroundNormalY);
            IsGrounded = count > 0 && Mathf.Abs(Core.Rigidbody.velocity.y) <= _minMoveDistance;

            if (Mathf.Abs(_velocityBuffer.x) > _minMoveDistance)
                Core.Transform.Translate(_velocityBuffer * Vector2.right * Time.deltaTime);

        }

        private void Update()
        {
            _velocityBuffer = _velocity;
            _velocity = Vector2.zero;

            if (IsGrounded && Mathf.Abs(_velocityBuffer.y) > _minMoveDistance)
                Core.Rigidbody.AddForce(_velocityBuffer * Vector2.up);
        }
    }
}
