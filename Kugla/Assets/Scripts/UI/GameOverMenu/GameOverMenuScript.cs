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
        private TextMeshProUGUI BestScore;

        [SerializeField]
        private TextMeshProUGUI LastScore;

        [SerializeField]
        private AudioSource GameOverAudio;
#pragma warning restore 0649

        private void Start()
        {
            BestScore.text = ScoreManagement.BestScore.ToString();
            LastScore.text = ScoreManagement.LastScore.ToString();

            if (ScoreManagement.ReachedNewBestScore)
            {
                var bestScoreLabel = GameObject.Find("BestScoreLabel").GetComponent<TextMeshProUGUI>();
                bestScoreLabel.enabled = false;
                BestScore.enabled = false;

                var lastScoreLabel = GameObject.Find("ScoreLabel").GetComponent<TextMeshProUGUI>();
                lastScoreLabel.text = "Your new best score";
                LastScore.transform.position += new Vector3(0, 70);
                lastScoreLabel.transform.position += new Vector3(0, 70);
            }
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