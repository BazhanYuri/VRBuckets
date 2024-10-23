using System;
using UnityEngine;

namespace Player
{
    public class Turn : MonoBehaviour
    {
        public Transform playerTransform;
        public PlayerInputHolder playerInputHolder;

        private void OnEnable()
        {
            playerInputHolder.OnTurn += RotatePlayer;
        }
        
        private void OnDisable()
        {
            playerInputHolder.OnTurn -= RotatePlayer;
        }

        private void RotatePlayer()
        {
            playerTransform.Rotate(Vector3.up, playerInputHolder.TurnValue * 3 * Time.deltaTime);
        }
    }
}