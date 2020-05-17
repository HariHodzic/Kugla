using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.UI.MainManu
{
    public class MainMenuScript : MonoBehaviour
    {
        public void Play()
        {
            SceneManager.LoadScene("GameScene");
        }

        public void Options()
        {
            SceneManager.LoadScene("OptionsMenu");
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}