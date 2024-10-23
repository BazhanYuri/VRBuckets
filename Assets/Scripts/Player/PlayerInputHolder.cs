using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerInputHolder : MonoBehaviour
    {
        private global::PlayerInput playerInput;
        
        public event Action OnTurn;
        
        public float TurnValue { get; private set; }

        private void OnEnable()
        {
            playerInput = new global::PlayerInput();

            playerInput.Player.Turn.performed += Turn;
        }
        
        private void OnDisable()
        {
            playerInput.Player.Turn.performed -= Turn;
        }

        private void Turn(InputAction.CallbackContext obj)
        {
            TurnValue = obj.ReadValue<float>();
            Debug.Log("Turn" + obj.ReadValue<float>());
        }
    }
}