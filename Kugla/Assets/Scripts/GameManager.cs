using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class GameManager : MonoBehaviour
    {
        private bool gameEnded = false;

        public void GameOver()
        {
            FindObjectOfType<Score>().GameOn = false;
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