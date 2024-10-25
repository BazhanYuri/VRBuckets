namespace Multiplayer.Models
{
    [RealtimeModel]
    public partial class ScoreModel
    {
        [RealtimeProperty(1, true, true)]
        private int _firstTeamScore;
        
        [RealtimeProperty(2, true, true)]
        private int _secondTeamScore;
        
    }
}