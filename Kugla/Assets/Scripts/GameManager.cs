using Assets.Scripts.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class GameManager : MonoBehaviour
    {
#pragma warning disable 0649
        private bool gameEnded = false;
        public static bool GameOn = true;
#pragma warning restore 0649

        public void GameOver()
        {
            GameOn = false;
            if (!gameEnded)
            {
                gameEnded = true;
                Invoke("Restart", 2f);
            }
        }

        private void Restart()
        {
            SceneManager.LoadScene("GameOverScene");
        }
    }
}