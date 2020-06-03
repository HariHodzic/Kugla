using Assets.Scripts.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class GameManager : MonoBehaviour
    {
#pragma warning disable 0649
        private static bool gameEnded;
        public static bool GameOn;
#pragma warning restore 0649

        private void Start()
        {
            GameOn = true;
            gameEnded = false;
        }
        public static void GameOver()
        {
            GameOn = false;
            if (!gameEnded)
            {
                gameEnded = true;
                Restart();
            }
        }

        private static void Restart()
        {
            SceneManager.LoadScene("GameOverScene");
        }
    }
}