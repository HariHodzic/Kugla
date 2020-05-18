using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.UI.MainManu
{
    public class MainMenuScript : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI BestScore;

        private void Start()
        {
            BestScore.text = ScoreManagement.BestScore.ToString();
        }

        public void Play()
        {
            SceneManager.LoadScene(Constants.GameScene);
        }

        public void Options()
        {
            SceneManager.LoadScene(Constants.OptionsMenuScene);
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}