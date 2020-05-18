using Assets.Scripts.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class GameManager : MonoBehaviour
    {
#pragma warning disable 0649
        private bool gameEnded = false;
#pragma warning restore 0649

        public void GameOver()
        {
            FindObjectOfType<ScoreManagement>().GameOn = false;
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