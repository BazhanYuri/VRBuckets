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
        public AreaThrowChecker currentThrowZoneChecker;
        
        private ScoreBoard _scoreBoard;
        private bool _isCanSpawn = true;
        public event Action BallExitedZoneOrThrown;

        private void OnEnable()
        {
            spawnAction.action.Enable();
            spawnAction.action.performed += OnSpawn;
            
            _scoreBoard.OnScoreChanged += AllowToThrow;
            _scoreBoard.OnTurnChanged += AllowToThrow;
        }

        private void OnDisable()
        {
            spawnAction.action.performed -= OnSpawn;
            
            _scoreBoard.OnScoreChanged -= AllowToThrow;
            _scoreBoard.OnTurnChanged -= AllowToThrow;
        }

        private void Awake()
        {
            _scoreBoard = FindObjectOfType<ScoreBoard>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Z))
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
            
            if (!_isCanSpawn)
            {
                return;
            }
            
            Ball ball = Realtime.Instantiate("Ball", spawnPoint.position, spawnPoint.rotation)
                .GetComponent<Ball>();

            ball.SetTeam(player.team);
            ball.OnBallExitedZoneOrThrown += OnBallExitedZoneOrThrown;
            
            _isCanSpawn = false;
        }

        private void OnBallExitedZoneOrThrown(Ball obj)
        {
            BallExitedZoneOrThrown?.Invoke();
            obj.currentThrowZone = currentThrowZoneChecker.CurrentZone;
            obj.OnBallExitedZoneOrThrown -= OnBallExitedZoneOrThrown;
        }

        private void AllowToThrow()
        {
            _isCanSpawn = true;
        }
    }
}