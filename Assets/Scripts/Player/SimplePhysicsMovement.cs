using Control;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class SimplePhysicsMovement : SerializedMonoBehaviour, IMovement
    {
        private const float _minMoveDistance = 0.001f;

        private readonly RaycastHit2D[] _hitBuffer = new RaycastHit2D[16];

        [SerializeField] private Vector2 _velocity;
        [SerializeField] private Vector2 _velocityBuffer;
        [SerializeField] private float _minGroundNormalY = .65f;
        [SerializeField] private LayerMask _layerMask;

        private ContactFilter2D _contactFilter;
        private Rigidbody2D _rigidbody;

        [ShowInInspector] public bool IsGrounded { get; private set; }

        public bool IsMove => Mathf.Abs(_velocityBuffer.x) > _minMoveDistance;
        public Vector3 Position => transform.position;
        public Vector2 Velocity => _velocityBuffer;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

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

            int count = _rigidbody.Cast(-transform.up, _contactFilter, _hitBuffer, _minGroundNormalY);
            IsGrounded = count > 0 && Mathf.Abs(_rigidbody.velocity.y) <= _minMoveDistance;

            if (IsMove)
                transform.Translate(_velocityBuffer * Vector2.right * Time.deltaTime);
        }

        private void Update()
        {
            _velocityBuffer = _velocity;
            _velocity = Vector2.zero;

            if (IsGrounded && Mathf.Abs(_velocityBuffer.y) > _minMoveDistance)
                _rigidbody.AddForce(_velocityBuffer * Vector2.up);
        }
    }
}
