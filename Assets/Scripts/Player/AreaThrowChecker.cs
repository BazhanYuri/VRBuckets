using DefaultNamespace;
using Multiplayer.Services;
using UI;
using UnityEngine;

namespace Player
{
    public class AreaThrowChecker : MonoBehaviour
    {
        public Player player;
        public UIComponent backToZonePopUp;
        
        private ScoreBoard _scoreBoard;
        
        private bool _isInZone = false;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out AreaThrow areaThrow))
            {
                _isInZone = true;
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out AreaThrow areaThrow))
            {
                _isInZone = false;
            }
        }

        private void Start()
        {
            _scoreBoard = FindObjectOfType<ScoreBoard>();
        }

        private void Update()
        {
            if (player.Index == _scoreBoard.CurrentTurnIndex && _isInZone == false)
            {
                backToZonePopUp.Show();
            }
            else
            {
                backToZonePopUp.Hide();
            }
        }
    }
}