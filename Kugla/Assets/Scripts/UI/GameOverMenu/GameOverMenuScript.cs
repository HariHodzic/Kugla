using Assets.Scripts.Score;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.UI.GameOverMenu
{
    public class GameOverMenuScript : MonoBehaviour
    {
#pragma warning disable 0649
        [SerializeField]
        private TextMeshProUGUI LastScore;

        [SerializeField]
        private AudioSource GameOverAudio;

        private TextMeshProUGUI LastScoreLabel;

#pragma warning restore 0649

        private void Start()
        {
            LastScore.text = ScoreManagement.LastScore.ToString();

            LastScoreLabel = GameObject.Find(Constants.GameOverScoreLabel).GetComponent<TextMeshProUGUI>();
            LastScoreLabel.text = ScoreManagement.ReachedNewBestScore ? "Your new best score" : "Your score was";
        }

        public void PlayAgain()
        {
            SceneManager.LoadScene(Constants.GameScene);
            GameManager.GameOn = true;
        }

        public void MainMenu()
        {
            SceneManager.LoadScene(Constants.MainMenuScene);
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}