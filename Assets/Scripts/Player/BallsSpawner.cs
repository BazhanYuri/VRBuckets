using System;
using Multiplayer.Services;
using Normal.Realtime;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class BallsSpawner : MonoBehaviour
    {
        public Player player;
        public InputActionReference spawnAction;
        public Transform spawnPoint;
        
        private ScoreBoard _scoreBoard;
        public event Action BallThrown;

        private void OnEnable()
        {
            spawnAction.action.Enable();
            spawnAction.action.performed += OnSpawn;
        }

        private void OnDisable()
        {
            spawnAction.action.performed -= OnSpawn;
        }

        private void Start()
        {
            _scoreBoard = FindObjectOfType<ScoreBoard>();
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
            if (player.Index != _scoreBoard.CurrentTurnIndex)
            {
                return;
            }
            
            Ball ball = Realtime.Instantiate("Ball", spawnPoint.position, spawnPoint.rotation)
                .GetComponent<Ball>();

            ball.SetTeam(player.team);
            ball.OnBallThrown += () => BallThrown?.Invoke();
        }
    }
}