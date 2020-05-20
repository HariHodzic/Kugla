using Assets.Scripts.Entities;
using Assets.Scripts.Helpers;
using Assets.Scripts.Instantiate;
using Assets.Scripts.Movement;
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

        private int Score;

        [SerializeField]
        private Text WallsRemainedValue;

        public static long BestScore { get; private set; } = 0;
        public static long LastScore { get; private set; } = 0;
        public static bool ReachedNewBestScore { get; private set; }

        private int LastXaxis = 0;
#pragma warning restore 0649

        private void Start()
        {
            ReachedNewBestScore = false;
            Score = 0;
        }

        private void Update()
        {
            if (!GameManager.GameOn)
                return;
            if (LastXaxis != (int)kugla.position.x)
            {
                LastXaxis = (int)kugla.position.x;

                Score += (int)(KuglaMovement.SideSpeed / 2 * (InstantiateWall.SuperWallEnabled ? 3 : 1));
            }
            ScoreValue.text = Score.ToString();
            WallsRemainedValue.text = InstantiateWall.WallsRemained.ToString();
        }

        private void OnDisable()
        {
            var score = new Score
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