using System;
using UnityEngine;

namespace Player
{
    public class MovePlayerBackToZone : MonoBehaviour
    {
        public Transform player;
        
        public float minX;
        public float maxX;
        public float minZ;
        public float maxZ;
        
        private void Update()
        {
            if (player.position.x < minX)
            {
                player.position = new Vector3(minX, player.position.y, player.position.z);
            }
            else if (player.position.x > maxX)
            {
                player.position = new Vector3(maxX, player.position.y, player.position.z);
            }
            else if (player.position.z < minZ)
            {
                player.position = new Vector3(player.position.x, player.position.y, minZ);
            }
            else if (player.position.z > maxZ)
            {
                player.position = new Vector3(player.position.x, player.position.y, maxZ);
            }
        }
    }
}