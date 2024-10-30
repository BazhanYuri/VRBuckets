using DG.Tweening;
using Normal.Realtime;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerJumper : MonoBehaviour
    {
        public InputActionReference jumpAction;
        public Transform player;
        public float jumpForce = 10f;
        public float jumpTime = 2f;
        
        public Ease ease = Ease.InOutSine;

        private bool isJumping = false;

        private void OnEnable()
        {
            jumpAction.action.Enable();
            jumpAction.action.performed += OnJump;
        }

        private void OnDisable()
        {
            jumpAction.action.performed -= OnJump;
        }

        private void OnJump(InputAction.CallbackContext obj)
        {
            if (isJumping) return;

            isJumping = true;
            player.DOJump(player.position, jumpForce, 1, jumpTime)
                .SetEase(ease)
                .OnComplete(() => isJumping = false);
        }
    }
}