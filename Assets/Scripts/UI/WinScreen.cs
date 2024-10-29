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
        public Multiplayer.Services.ScoreBoard scoreBoard;
        public Transform vfxRoot;
        
        private bool _countDownStarted = false;


        public void SetWinner(int index)
        {
            winText.text = index == 0 ? "Player 1 Wins!" : "Player 2 Wins!";
        }

        public override void Show()
        {
            base.Show();
            if (_countDownStarted == true)
            {
                return;
            }
            _countDownStarted = true;
            vfxRoot.gameObject.SetActive(true);
            StartCoroutine(ReloadScene());
        }

        private IEnumerator ReloadScene()
        {
            int countDown = 10;
            while (countDown > 0)
            {
                countDownText.text = "Game restarts in: " + countDown;
                yield return new WaitForSeconds(1);
                countDown--;
            }

            scoreBoard.ResetScore();
            SceneManager.LoadScene(0);
        }
    }
}