using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class WinScreen : UIComponent
    {
        public TextMeshProUGUI winText;
        public TextMeshProUGUI countDownText;


        public void SetWinner(int index)
        {
            winText.text = index == 0 ? "Player 1 Wins!" : "Player 2 Wins!";
        }
        public override void Show()
        {
            base.Show();

            StartCoroutine(ReloadScene());
        }

        private IEnumerator ReloadScene()
        {
            int countDown = 5;
            while (countDown > 0)
            {
                countDownText.text = "Game starts in: " + countDown;
                yield return new WaitForSeconds(1);
                countDown--;
            }
            SceneManager.LoadScene(0);
        }
    }
}