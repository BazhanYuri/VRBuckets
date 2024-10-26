using TMPro;
using UnityEngine;

namespace UI.ScoreBoard
{
    public class PlayerForm : MonoBehaviour
    {
        public TextMeshProUGUI playerName;
        
        public void SetPlayerName(string name)
        {
            playerName.text = name;
        }
    }
}