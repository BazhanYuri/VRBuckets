using System;
using UnityEngine;

namespace Player
{
    public class Player : MonoBehaviour
    {
        public Camera vrCamera;

        private void Awake()
        {
            vrCamera.enabled = true;
        }
    }
}