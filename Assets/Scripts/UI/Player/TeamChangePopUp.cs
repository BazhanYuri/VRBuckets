using Enums;
using TMPro;

namespace UI.Player
{
    public class TeamChangePopUp : UIPopUp
    {
        public TextMeshProUGUI text;
        
        public void SetText(Team team)
        {
            base.Show();
        }
    }
}