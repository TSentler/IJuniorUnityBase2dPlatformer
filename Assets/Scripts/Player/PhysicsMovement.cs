using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PhysicsMovement : MonoBehaviour, IMovement
    {
        private const float _minMoveDistance = 0.001f;
        private const float _shellRadius = 0.01f;

        private readonly RaycastHit2D[] _hitBuffer = new RaycastHit2D[16];

        [SerializeField] private Vector2 _velocity;
        [SerializeField] private float _minGroundNormalY = .65f;
        [SerializeField] private float _gravityModifier = 1f;
        [SerializeField] private LayerMask _layerMask;

        private Vector2 _groundNormal;
        private ContactFilter2D _contactFilter;
        private List<RaycastHit2D> _hitBufferList = new List<RaycastHit2D>(16);
        private Rigidbody2D _rigidbody;

        [ShowInInspector] public bool IsGrounded { get; private set; }

        public bool IsMove => Mathf.Abs(_velocity.x) > _minMoveDistance;
        public Vector3 Position => transform.position;
        public Vector2 Velocity => _velocity;


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
            _velocity.y += _gravityModifier * Physics2D.gravity.y * Time.deltaTime;

            IsGrounded = false;

            Vector2 deltaPosition = _velocity * Time.deltaTime;
            Vector2 moveAlongGround = new Vector2(_groundNormal.y, -_groundNormal.x);
            Vector2 move = moveAlongGround * deltaPosition.x;

            Movement(move, false);

            move = Vector2.up * deltaPosition.y;

            Movement(move, true);
        }

        private void Movement(Vector2 move, bool yMovement)
        {
            float distance = move.magnitude;

            if (distance > _minMoveDistance)
            {
                int count = _rigidbody.Cast(move, _contactFilter, _hitBuffer, distance + _shellRadius);

                _hitBufferList.Clear();

                for (int i = 0; i < count; i++)
                {
                    _hitBufferList.Add(_hitBuffer[i]);
                }

                for (int i = 0; i < _hitBufferList.Count; i++)
                {
                    Vector2 currentNormal = _hitBufferList[i].normal;
                    if (currentNormal.y > _minGroundNormalY)
                    {
                        IsGrounded = true;
                        if (yMovement)
                        {
                            _groundNormal = currentNormal;
                            currentNormal.x = 0;
                        }
                    }

                    float projection = Vector2.Dot(_velocity, currentNormal);
                    if (projection < 0)
                    {
                        _velocity = _velocity - projection * currentNormal;
                    }

                    float modifiedDistance = _hitBufferList[i].distance - _shellRadius;
                    distance = modifiedDistance < distance ? modifiedDistance : distance;
                }
            }

            _rigidbody.position += move.normalized * distance;
        }
    }
}
