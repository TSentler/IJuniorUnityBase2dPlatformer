using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Control;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
    public class DefaultCore : SerializedMonoBehaviour, IPlayerCore
    {
        private bool _isInitialized;

        [ShowInInspector] public IInputHandler InputHandler { get; private set; }

        public bool IsInitialized => _isInitialized;
        public GameObject GameObject => gameObject;
        public Transform Transform => transform;
        public Rigidbody2D Rigidbody { get; private set; }
        public Animator Animator { get; private set; }

        public void Initialize(IInputHandler inputHandler, Rigidbody2D rigidbody, Animator animator)
        {
            if (_isInitialized)
                return;

            InputHandler = inputHandler;
            Rigidbody = rigidbody;
            Animator = animator;

            _isInitialized = true;
        }

        private void Awake()
        {
            if (_isInitialized == false)
                gameObject.SetActive(false);
        }

    }

    
}
