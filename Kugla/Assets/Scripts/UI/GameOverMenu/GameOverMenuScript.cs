using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.UI.GameOverMenu
{
    public class GameOverMenuScript : MonoBehaviour
    {
        public void PlayAgain()
        {
            SceneManager.LoadScene("GameScene");
        }

        public void MainMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }
        public void Quit()
        {
            Application.Quit();
        }
    }
}
