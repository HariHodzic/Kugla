using Assets.Scripts.Score;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

namespace Assets.Scripts.Movement
{
    public class KuglaMovement : ScoreData
    {
#pragma warning disable 0649
        private PlayerPrefs PlayerPrefs;
        private GameManager GameManager;

        [SerializeField]
        private Rigidbody kugla;

        private bool LeftSide = true;

        [SerializeField]
        private AudioSource BallBounceAudio;

        [SerializeField]
        private AudioSource SuperObstacleCollisionAudio;

        [SerializeField]
        private GameObject Terrain;

        [SerializeField]
        private GameObject WallInstantiateZoneRight;

        [SerializeField]
        private GameObject WallInstantiateZoneLeft;

        private Slider SpeedSlider;

        private Vector3 LastKuglaPosition;

        public static float ForwardSpeed { get; private set; }
        public static float SideSpeed { get; private set; }

        private float tempTime = 2f;

        public static int BasicBounces { get; private set; }
#pragma warning restore 0649


        private void GoLeft(Rigidbody rb)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.AddForce(2 * ForwardSpeed, 0, 2 * SideSpeed, ForceMode.VelocityChange);
        }

        private void GoRight(Rigidbody rb)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.AddForce(2 * ForwardSpeed, 0, -2 * SideSpeed, ForceMode.VelocityChange);
        }

        private void Start()
        {
            BasicBounces = 0;
            SideSpeed = PlayerPrefs.GetFloat("SideSpeed");
            ForwardSpeed = PlayerPrefs.GetFloat("ForwardSpeed");

            kugla.AddForce(2 * ForwardSpeed, 0, 5, ForceMode.VelocityChange);
            GameManager = FindObjectOfType<GameManager>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            var basicWallCondition = collision.collider.tag == Constants.WallTag;
            if (basicWallCondition || collision.collider.tag == Constants.SuperWallTag)
            {
                if (LeftSide)
                    GoRight(kugla);
                else
                    GoLeft(kugla);
                LeftSide = !LeftSide;

                BallBounceAudio.Play();
                if (BasicWallsRemained == 0)
                    GameManager.GameOver();

                if (basicWallCondition)
                    BasicBounces++;
                if (BasicBounces == 6)
                    BasicBounces = 1;
            }

            if (collision.collider.tag == Constants.SuperObstacleTag)
            {
                SuperObstacleCollisionAudio.Play();
                int superWallsToAdd = 3 - Level;

                SuperWallsRemained += superWallsToAdd < 1 ? 1 : superWallsToAdd;
            }
            else if (collision.collider.tag == Constants.ObstacleTag)
            {
                int wallsToAdd = 4 - Level;
                BasicWallsRemained += wallsToAdd < 1 ? 1 : wallsToAdd;
            }
        }

        private void FixedUpdate()
        {
            if (LastKuglaPosition == transform.position)
            {
                GameManager.GameOver();
            }
            LastKuglaPosition = transform.position;

            if (kugla.transform.position.y < 0)
                GameManager.GameOver();

            if (kugla.transform.position.x % 10 < 1 && Time.time - tempTime > 3f)
            {
                tempTime = Time.time;
                var vectorToAdd = new Vector3(10, 0, 0);
                Terrain.transform.position += vectorToAdd;
                WallInstantiateZoneLeft.transform.position += vectorToAdd;
                WallInstantiateZoneRight.transform.position += vectorToAdd;
            }
        }

        public void AdjustSideSpeed(float speed)
        {
            PlayerPrefs.SetFloat("SideSpeed", speed);
            PlayerPrefs.Save();
        }

        public void AdjustForwardSpeed(float speed)
        {
            PlayerPrefs.SetFloat("ForwardSpeed", speed);
            PlayerPrefs.Save();
        }
    }
}