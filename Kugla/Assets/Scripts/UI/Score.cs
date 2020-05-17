using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class Score : MonoBehaviour
    {
#pragma warning disable 0649

        [SerializeField]
        private Transform kugla;

        [SerializeField]
        private Text ScoreValue;

        public bool GameOn = true;

#pragma warning restore 0649

        private void Update()
        {
            if (GameOn)
                ScoreValue.text = kugla.position.x.ToString("0");
        }
    }
}