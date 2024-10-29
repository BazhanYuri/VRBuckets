using System;
using DefaultNamespace;
using Multiplayer.Models;
using Normal.Realtime;
using TMPro;
using UI;
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
        
        public WinScreen winScreen;
        
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

        public void IncrementScoreForFirstTeam(Ball ball)
        {
            if (realtimeTransform.isOwnedLocally)
            {
                ThrowScoreZone throwScoreZone = FindCurrentZoneInPlayer();
                model.firstTeamScore += throwScoreZone.score;
                OnScoreChanged?.Invoke();
                
                if (model.firstTeamScore >= 7)
                {
                    winScreen.SetWinner(0);
                    winScreen.Show();
                    model.playerTurnIndex = -1;
                    model.firstTeamScore = 0;
                    model.secondTeamScore = 0;
                    gameObject.SetActive(false);
                }
            }
        }

        public void IncrementScoreForSecondTeam(Ball ball)
        {
            if (realtimeTransform.isOwnedLocally)
            {
                ThrowScoreZone throwScoreZone = FindCurrentZoneInPlayer();
                model.secondTeamScore +=  throwScoreZone.score;
                OnScoreChanged?.Invoke();
                
                if (model.secondTeamScore >= 7)
                {
                    winScreen.SetWinner(1);
                    winScreen.Show();
                    model.playerTurnIndex = -1;
                    model.firstTeamScore = 0;
                    model.secondTeamScore = 0;
                    gameObject.SetActive(false);
                }
            }
        }

        private ThrowScoreZone FindCurrentZoneInPlayer()
        {
            Player.Player[] players = FindObjectsOfType<Player.Player>();
            
            foreach (var player in players)
            {
                if (player.Index == model.playerTurnIndex)
                {
                    return player.areaThrowChecker.CurrentZone;
                }
            }
            
            return null;
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