using UnityEngine;
using UnityEngine.SceneManagement;

namespace Multiplayer.Services
{
    public class LevelLoader : MonoBehaviour
    {
        public ScoreBoard scoreBoard;
        
        public void RestartGame()
        {
            scoreBoard.ResetScore();
            SceneManager.LoadScene(0);
        }
    }
}