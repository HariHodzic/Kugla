using Assets.Scripts.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class GameManager : MonoBehaviour
    {
#pragma warning disable 0649
        private static bool gameEnded = false;
        public static bool GameOn = true;
#pragma warning restore 0649

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