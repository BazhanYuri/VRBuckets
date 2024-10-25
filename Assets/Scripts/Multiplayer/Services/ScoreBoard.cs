using Multiplayer.Models;
using Normal.Realtime;
using TMPro;
using UnityEngine.Serialization;

namespace Multiplayer.Services
{
    public class ScoreBoard : RealtimeComponent<ScoreModel>
    {
        public TextMeshProUGUI firstTeamScoreText;
        public TextMeshProUGUI secondTeamScoreText;
        
        private int _currentScoreFirstTeam = 0;
        private int _currentScoreSecondTeam = 0;
        
        public void IncrementScoreForFirstTeam()
        {
            model.firstTeamScore += 1;
        }
        
        public void IncrementScoreForSecondTeam()
        {
            model.secondTeamScore += 1;
        }
        
        protected override void OnRealtimeModelReplaced(ScoreModel previousModel, ScoreModel currentModel) {
            if (previousModel != null) {
                previousModel.firstTeamScoreDidChange -= ScoreDidChange;
                previousModel.secondTeamScoreDidChange -= ScoreDidChange;
            }
        
            if (currentModel != null) {
                if (currentModel.isFreshModel)
                {
                    currentModel.firstTeamScore = _currentScoreFirstTeam;
                    currentModel.secondTeamScore = _currentScoreFirstTeam;
                }
                

                UpdateScore();

                currentModel.firstTeamScoreDidChange += ScoreDidChange;
                currentModel.secondTeamScoreDidChange += ScoreDidChange;
            }
        }

        private void ScoreDidChange(ScoreModel model, int value) 
        {
            UpdateScore();
        }

        private void UpdateScore()
        {
            _currentScoreFirstTeam = model.firstTeamScore;
            _currentScoreSecondTeam = model.secondTeamScore;
            
            UpdateUI();
        }

        private void UpdateUI()
        {
            firstTeamScoreText.text = _currentScoreFirstTeam.ToString();
            secondTeamScoreText.text = _currentScoreSecondTeam.ToString();
        }
    }
}