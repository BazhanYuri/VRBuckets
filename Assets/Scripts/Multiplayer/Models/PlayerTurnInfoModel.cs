namespace Multiplayer.Models
{
    [RealtimeModel]
    public partial class PlayerTurnInfoModel
    {
        [RealtimeProperty(1, true, true)]
        private int _firstTeamScore;
        
        [RealtimeProperty(2, true, true)]
        private int _secondTeamScore;
        
        [RealtimeProperty(3, true, true)]
        private int _playerTurnIndex;
    }
}