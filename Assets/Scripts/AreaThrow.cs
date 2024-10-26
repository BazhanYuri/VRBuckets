using Normal.Realtime;
using UnityEngine;

namespace DefaultNamespace
{
    public class AreaThrow : MonoBehaviour
    {
        public RealtimeView realtimeView;
        public ZoneCondition[] zoneConditions;

        private int _currentScore;
        
        private void UpdateZonePosition(int score)
        {
            _currentScore = score;
        }

        private void UpdateZonePosition()
        {
            foreach (var zoneCondition in zoneConditions)
            {
                if (_currentScore >= zoneCondition.scores)
                {
                    transform.position = zoneCondition.zone.position;
                }
            }
        }
        
        [System.Serializable]
        public struct ZoneCondition
        {
            public Transform zone;
            public int scores;
        }
    }
}