using UnityEngine;

namespace Assets.Scripts.Score
{
    public class ScoreData : MonoBehaviour
    {
        public static int BasicWallsRemained { get; protected set; }
        public static int SuperWallsRemained { get; protected set; }
        public static int Score { get; protected set; }
        public static int Level { get; protected set; }
        public static bool SuperWallsExternalTrigger { get; protected set; }

        protected static void AddScore(int value) => Score += value;
    }
}