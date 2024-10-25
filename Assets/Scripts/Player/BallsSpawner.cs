using System;
using Multiplayer.Services;
using Normal.Realtime;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class BallsSpawner : MonoBehaviour
    {
        public TeamManager teamManager;
        public InputActionReference spawnAction;
        public Transform spawnPoint;

        private void OnEnable()
        {
            spawnAction.action.Enable();
            spawnAction.action.performed += OnSpawn;
        }

        private void OnDisable()
        {
            spawnAction.action.performed -= OnSpawn;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                SpawnBall();
            }
        }

        private void OnSpawn(InputAction.CallbackContext context)
        {
            SpawnBall();
        }

        private void SpawnBall()
        {
            Ball ball = Realtime.Instantiate("Ball", spawnPoint.position, spawnPoint.rotation)
                .GetComponent<Ball>();

            ball.SetTeam(teamManager.playerTeam);
        }
    }
}