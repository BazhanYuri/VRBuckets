using UnityEngine;

namespace UI.ScoreBoard
{
    public class TeamUI : MonoBehaviour
    {
        public PlayerForm playerFormPrefab;
        public RectTransform playerFormsRoot;
        
        public void DeleteAllPlayerForms()
        {
            foreach (Transform child in playerFormsRoot)
            {
                Destroy(child.gameObject);
            }
        }
        
        public void AddPlayerForm(string playerName)
        {
            PlayerForm playerForm = Instantiate(playerFormPrefab, playerFormsRoot);
            playerForm.SetPlayerName(playerName);
        }
    }
}