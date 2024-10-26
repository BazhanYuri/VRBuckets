using Enums;
using TMPro;

namespace UI.Player
{
    public class TeamChangePopUp : UIPopUp
    {
        public TextMeshProUGUI text;
        
        public string firstTeamName;
        public string secondTeamName;

        public void SetText(Team team)
        {
            base.Show();

            string textTemp = team == Team.First ? firstTeamName : secondTeamName;
            
            text.text = string.Format(text.text, textTemp);
        }
    }
}