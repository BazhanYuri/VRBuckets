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

        private void Update()
        {
            //transform.position += new Vector3(0, 1, 0) * Time.deltaTime;
        }
    }
}