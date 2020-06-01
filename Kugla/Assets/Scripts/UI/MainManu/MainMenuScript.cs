using Assets.Scripts.Score;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.UI.MainManu
{
    public class MainMenuScript : MonoBehaviour
    {
        #pragma warning disable 0649
        [SerializeField]
        private TextMeshProUGUI BestScore;

        [SerializeField]
        private Text MobileModeText;

        public static int MobileMode { get; private set; }

        #pragma warning restore 0649
        private void Start()
        {
            MobileMode = 0;
            BestScore.text = ScoreManagement.BestScore.ToString();
            if (PlayerPrefs.HasKey("MobileMode"))
            {
                var mobileModeFromPrefs = PlayerPrefs.GetInt("MobileMode");
                MobileMode = mobileModeFromPrefs;
            }
            else
            {
                PlayerPrefs.SetInt("MobileMode",0);
            }
            MobileModeText.text = MobileMode == 0 ? "OFF" : "ON";
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

        public void MobileModeToggle()
        {
            MobileMode = MobileMode == 0 ? 1 : 0;
            PlayerPrefs.SetInt("MobileMode",MobileMode);
            MobileModeText.text = MobileMode == 0 ? "OFF" : "ON";
        }
    }
}