using System;
using UnityEngine;

namespace Assets.Scripts.Settings
{
    [Serializable]
    public class GameSettings
    {
        [SerializeField]
        [Range(1, 100)]
        public float ForwardSpeed;

        [SerializeField]
        [Range(1, 100)]
        public float SideSpeed;

        public void AdjustForwardSpeed(float speed)
        {
            ForwardSpeed = speed;
        }

        public void AdjustSideSpeed(float speed)
        {
            SideSpeed = speed;
        }
    }
}