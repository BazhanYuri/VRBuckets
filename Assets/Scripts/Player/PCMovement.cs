using UnityEngine;

namespace Player
{
    public class PCMovement : MonoBehaviour
    {
        public Transform player;
        public float speed;
        
        private void Update()
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            Vector3 direction = new Vector3(horizontal, 0, vertical);
            player.Translate(direction * (speed * Time.deltaTime));
        }

    }
}