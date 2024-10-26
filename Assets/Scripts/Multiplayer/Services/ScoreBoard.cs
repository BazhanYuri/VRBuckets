using System;
using Multiplayer.Models;
using Normal.Realtime;
using TMPro;
using UI.ScoreBoard;

namespace Multiplayer.Services
{
    public class ScoreBoard : RealtimeComponent<PlayerTurnInfoModel>
    {
        public RealtimeTransform realtimeTransform;
        public TextMeshProUGUI firstTeamScoreText;
        public TextMeshProUGUI secondTeamScoreText;
        
        public TextMeshProUGUI firstTeamCountText;
        public TextMeshProUGUI secondTeamCountText;
        
        public TextMeshProUGUI countDownText;
        public TextMeshProUGUI whoseTurnText;
        
        public TeamUI firstTeamUI;
        public TeamUI secondTeamUI;
        
        private int _currentScoreFirstTeam = 0;
        private int _currentScoreSecondTeam = 0;
        private int _currentTurnIndex = 0;
        
        public event Action OnScoreChanged; 

        public int CurrentTurnIndex
        {
            get => _currentTurnIndex;
            set
            {
                _currentTurnIndex = value;
                model.playerTurnIndex = value;
            }
        }

        public void IncrementScoreForFirstTeam()
        {
            if (realtimeTransform.isOwnedLocally)
            {
                model.firstTeamScore += 1;
                OnScoreChanged?.Invoke();
            }
        }
        
        public void IncrementScoreForSecondTeam()
        {
            if (realtimeTransform.isOwnedLocally)
            {
                model.secondTeamScore += 1;
                OnScoreChanged?.Invoke();
            }
        }
        
        public void UpdateTeamCount(int first, int second)
        {
            firstTeamCountText.text = "Count: " + first.ToString();
            secondTeamCountText.text = "Count: " + second.ToString();
        }
        
        protected override void OnRealtimeModelReplaced(PlayerTurnInfoModel previousModel, PlayerTurnInfoModel currentModel) {
            if (previousModel != null) {
                previousModel.firstTeamScoreDidChange -= Changed;
                previousModel.secondTeamScoreDidChange -= Changed;
                previousModel.playerTurnIndexDidChange -= Changed;
            }
        
            if (currentModel != null) {
                if (currentModel.isFreshModel)
                {
                    currentModel.firstTeamScore = _currentScoreFirstTeam;
                    currentModel.secondTeamScore = _currentScoreFirstTeam;
                    _currentTurnIndex = currentModel.playerTurnIndex;
                }
                

                UpdateValues();

                currentModel.firstTeamScoreDidChange += Changed;
                currentModel.secondTeamScoreDidChange += Changed;
                currentModel.playerTurnIndexDidChange += Changed;
            }
        }

        private void Changed(PlayerTurnInfoModel model, int value) 
        {
            UpdateValues();
        }

        private void UpdateValues()
        {
            _currentScoreFirstTeam = model.firstTeamScore;
            _currentScoreSecondTeam = model.secondTeamScore;
            
            _currentTurnIndex = model.playerTurnIndex;
            
            UpdateUI();
        }

        private void UpdateUI()
        {
            firstTeamScoreText.text = _currentScoreFirstTeam.ToString();
            secondTeamScoreText.text = _currentScoreSecondTeam.ToString();

            UpdatePlayerTurn();
        }
        
        private void UpdatePlayerTurn()
        {
            whoseTurnText.text = "Player " + (_currentTurnIndex + 1) + "'s turn";
        }
    }
}