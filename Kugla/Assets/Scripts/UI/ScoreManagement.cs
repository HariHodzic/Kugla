using Assets.Scripts.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class ScoreManagement : MonoBehaviour
    {
#pragma warning disable 0649

        [SerializeField]
        private Transform kugla;

        [SerializeField]
        private Text ScoreValue;

        public static long BestScore { get; private set; } = 0;
        public static long LastScore { get; private set; } = 0;
        public static bool ReachedNewBestScore { get; private set; }

        public bool GameOn { get; set; } = true;

#pragma warning restore 0649

        private void Start()
        {
            ScoreValue.color = new Color(45, 46, 45);
            ReachedNewBestScore = false;
        }

        private void Update()
        {
            if (GameOn)
                ScoreValue.text = kugla.position.x.ToString("0");
        }

        private void OnDisable()
        {
            var score = new Score
            {
                ScoreAmount = int.Parse(ScoreValue.text)
            };

            var allRecScores = GetAllResults();

            if (score.ScoreAmount > LastScore)
            {
                BestScore = score.ScoreAmount;
                ReachedNewBestScore = true;
            }
            LastScore = score.ScoreAmount;

            allRecScores.Add(score);
            allRecScores = allRecScores.OrderByDescending(x => x.ScoreAmount).ToList();

            var scoresToSave = JsonConvert.SerializeObject(allRecScores);
            PlayerPrefs.SetString(Constants.PlayerResults, scoresToSave);
            PlayerPrefs.Save();
        }

        private List<Score> GetAllResults()
        {
            var allRecordedScoresJson = PlayerPrefs.GetString(Constants.PlayerResults);
            var allRecScores = new List<Score>();

            if (!string.IsNullOrWhiteSpace(allRecordedScoresJson) && allRecordedScoresJson != "{}")
            {
                allRecScores = JsonConvert.DeserializeObject<List<Score>>(allRecordedScoresJson);
            }

            return allRecScores;
        }
    }
}