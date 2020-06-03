using Assets.Scripts.Score;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;
using Slider = UnityEngine.Experimental.UIElements.Slider;

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

        public static bool SuperWallsEnabled { get; private set; }

        //4 seconds for 5 basic walls to reach super walls
        private float superWallTargetTime = 4.0f;

        private float superWallTempTime;

        [SerializeField]
        private GameObject SuperWallGoalBarMeter;

        [SerializeField]
        private GameObject SuperWallGoalBar;

        [SerializeField]
        private Image SuperWallGoalBarContainer;

        private float SuperWallGoalBarHeight;
        private Text WallsRemainedLabel;
        private Text WallsRemainedValue;

        [SerializeField] private AudioSource SuperWallStartAudio;

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
            WallsRemainedLabel = GameObject.Find(Constants.WallsRemainedLabel).GetComponent<Text>();
            WallsRemainedValue = GameObject.Find(Constants.WallsRemainedValue).GetComponent<Text>();

            SuperWallsEnabled = false;
            BasicBounces = 0;
            SideSpeed = PlayerPrefs.GetFloat("SideSpeed");
            ForwardSpeed = PlayerPrefs.GetFloat("ForwardSpeed");

            kugla.AddForce(2 * ForwardSpeed, 0, 5, ForceMode.VelocityChange);
            GameManager = FindObjectOfType<GameManager>();

            SuperWallGoalBarHeight = SuperWallGoalBarMeter.transform.localScale.y;
            SuperWallGoalBarMeter.transform.localScale -= new Vector3(0, SuperWallGoalBarMeter.transform.localScale.y, 0);
        }

        private void OnCollisionEnter(Collision collision)
        {
            var basicWallCondition = collision.collider.tag == Constants.WallTag;
            var superWallCondition = collision.collider.tag == Constants.SuperWallTag;
            if (basicWallCondition || superWallCondition)
            {
                superWallTargetTime -= Time.deltaTime;

                if (!SuperWallsEnabled)
                {
                    SuperWallGoalBarMeter.transform.localScale += new Vector3(0, SuperWallGoalBarHeight / 5, 0);
                }

                if (LeftSide)
                    GoRight(kugla);
                else
                    GoLeft(kugla);
                LeftSide = !LeftSide;

                BallBounceAudio.Play();
                if (BasicWallsRemained == 0)
                    GameManager.GameOver();

                if (basicWallCondition)
                {
                    if (++BasicBounces == 5)
                    {
                        if (Time.time - superWallTempTime <= 5)
                        {
                            StartSuperWalls();
                        }
                        SuperWallGoalBarMeter.transform.localScale -= new Vector3(0, SuperWallGoalBarMeter.transform.localScale.y, 0);
                        superWallTargetTime = 4f;
                        BasicBounces = 0;
                    }
                    else if (BasicBounces == 1)
                        superWallTempTime = Time.time;

                    if (BasicBounces == 6)
                        BasicBounces = 1;
                }

                if (superWallCondition && SuperWallsRemained == 0)
                {
                    StartBasicWalls();
                }
            }

            if (collision.collider.tag == Constants.SuperObstacleTag)
            {
                SuperObstacleCollisionAudio.Play();
                int superWallsToAdd = 3 - Level;

                SuperWallsRemained += superWallsToAdd < 1 ? 2 : superWallsToAdd;
            }
            else if (collision.collider.tag == Constants.ObstacleTag)
            {
                int wallsToAdd = 4 - Level;
                BasicWallsRemained += wallsToAdd < 1 ? 2 : wallsToAdd;
            }
        }

        private void FixedUpdate()
        {
            if (LastKuglaPosition == transform.position)
            {
                GameManager.GameOver();
            }
            LastKuglaPosition = transform.position;

            if (kugla.transform.position.y < 0 || kugla.transform.position.y > 6.1)
                GameManager.GameOver();

            if (kugla.transform.position.x % 10 < 1 && Time.time - tempTime > 3f)
            {
                tempTime = Time.time;
                var vectorToAdd = new Vector3(6, 0, 0);
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

        private void StartSuperWalls()
        {
            SuperWallGoalBar.SetActive(false);
            SuperWallsEnabled = true;
            WallsRemainedLabel.alignment = TextAnchor.MiddleLeft;
            WallsRemainedValue.alignment = TextAnchor.MiddleCenter;
            WallsRemainedValue.font.material.color = Constants.DarkGoldColor;
            WallsRemainedLabel.font.material.color = Constants.DarkGoldColor;
            WallsRemainedLabel.text = "Super walls:";
            SuperWallStartAudio.Play();
        }

        private void StartBasicWalls()
        {
            SuperWallsEnabled = false;
            SuperWallGoalBar.SetActive(true);
            WallsRemainedLabel.alignment = TextAnchor.MiddleCenter;
            WallsRemainedValue.alignment = TextAnchor.MiddleLeft;
            WallsRemainedValue.font.material.color = Constants.FineBlackColor;
            WallsRemainedLabel.font.material.color = Constants.FineBlackColor;
            WallsRemainedLabel.text = "Walls";
        }
    }
}