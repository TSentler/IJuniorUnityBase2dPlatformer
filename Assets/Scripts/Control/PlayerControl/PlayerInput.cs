using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static PlayerControls;

namespace Control
{
    public class PlayerInput : MonoBehaviour, IPlayerInput, IPlayerActions
    {
        private PlayerControls _input;

        public Vector2 MovementDirection => _input.Player.Movement.ReadValue<Vector2>();
        public event UnityAction JumpStarted;

        private void Awake()
        {
            _input = new PlayerControls();
            _input.Player.SetCallbacks(this);
        }

        private void OnEnable()
        {
            _input.Enable();
        }

        private void OnDisable()
        {
            _input.Disable();
        }

        public void OnMovement(InputAction.CallbackContext context)
        {

        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                JumpStarted?.Invoke();
            }
        }
    }
}
