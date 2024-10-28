using DefaultNamespace;
using Multiplayer.Services;
using UI;
using UnityEngine;

namespace Player
{
    public class AreaThrowChecker : MonoBehaviour
    {
        private ThrowScoreZone _currentZone;

        public ThrowScoreZone CurrentZone
        {
            get => _currentZone;
            set => _currentZone = value;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out ThrowScoreZone areaThrow))
            {
                _currentZone = areaThrow;
            }
        }
    }
}