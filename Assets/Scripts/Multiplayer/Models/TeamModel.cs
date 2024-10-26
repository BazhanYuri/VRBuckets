namespace Multiplayer.Models
{
    [RealtimeModel]
    public partial class TeamModel
    {
        [RealtimeProperty(3, true, true)]
        private int _firstTeamCount;
        
        [RealtimeProperty(4, true, true)]
        private int _secondTeamCount;
        
        [RealtimeProperty(5, true, true)]
        private int _playerTurnIndex;
    }
}