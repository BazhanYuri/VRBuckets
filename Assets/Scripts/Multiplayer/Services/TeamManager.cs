using Enums;
using Multiplayer.Models;
using Normal.Realtime;

namespace Multiplayer.Services
{
    public class TeamManager : RealtimeComponent<TeamModel>
    {
        private int _team1Count = 0;
        private int _team2Count = 0;
    
       
        public Team playerTeam;

        private void Start()
        {
            _team1Count = model.firstTeamCount;
            _team2Count = model.secondTeamCount;
            AssignTeam();
        }
        
        protected override void OnRealtimeModelReplaced(TeamModel previousModel, TeamModel currentModel) {
            if (previousModel != null) {
                previousModel.firstTeamCountDidChange -= TeamCountChanged;
                previousModel.secondTeamCountDidChange -= TeamCountChanged;
            }
        
            if (currentModel != null) {
                if (currentModel.isFreshModel)
                {
                    currentModel.firstTeamCount = _team1Count;
                    currentModel.secondTeamCount = _team2Count;
                }
                
                UpdateTeamCount();


                currentModel.firstTeamCountDidChange += TeamCountChanged;
                currentModel.secondTeamCountDidChange += TeamCountChanged;
            }
        }

        private void UpdateTeamCount()
        {
            _team1Count = model.firstTeamCount;
            _team2Count = model.secondTeamCount;
        }

        private void TeamCountChanged(TeamModel teamModel, int value)
        {
            _team1Count = teamModel.firstTeamCount;
            _team2Count = teamModel.secondTeamCount;
        }

        private void AssignTeam()
        {
            if (_team1Count <= _team2Count)
            {
                playerTeam = Team.First;
                _team1Count++;
            }
            else
            {
                playerTeam = Team.Second;
                _team2Count++;
            }
            
            model.firstTeamCount = _team1Count;
            model.secondTeamCount = _team2Count;
        }
    }

}