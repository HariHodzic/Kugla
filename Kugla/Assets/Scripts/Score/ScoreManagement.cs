using Assets.Scripts.Helpers;
using Assets.Scripts.Movement;
using Assets.Scripts.WallScripts;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Score
{
    //ScoreData inherits MonoBehaviour
    public class ScoreManagement : ScoreData
    {
#pragma warning disable 0649

        [SerializeField]
        private Transform kugla;

        [SerializeField]
        private Text ScoreValue;

        [SerializeField]
        private Text WallsRemainedValue;

        public static long BestScore { get; private set; }
        public static long LastScore { get; private set; }
        public static bool ReachedNewBestScore { get; private set; }

        private int LastXaxis = 0;
#pragma warning restore 0649

        private void Start()
        {
            ReachedNewBestScore = false;
            Score = 0;
            Level = 1;
        }

        private void Update()
        {
            if (!GameManager.GameOn)
                return;
            if (LastXaxis != (int)kugla.position.x)
            {
                LastXaxis = (int)kugla.position.x;

                Score += (int)KuglaMovement.SideSpeed / 2;

                Level = Score / 1000;
            }
            ScoreValue.text = Score.ToString();
            WallsRemainedValue.text = KuglaMovement.SuperWallsEnabled?SuperWallsRemained.ToString():BasicWallsRemained.ToString();
        }

        private void OnDisable()
        {
            var score = new Entities.Score
            {
                ScoreAmount = Score
            };

            if (score.ScoreAmount > LastScore)
            {
                BestScore = score.ScoreAmount;
                ReachedNewBestScore = true;
            }
            LastScore = score.ScoreAmount;

            DataManager.AppendList(score, Constants.PlayerResults, nameof(Assets.Scripts.Entities.Score.ScoreAmount), OrderBy.DESC);
        }
    }
}