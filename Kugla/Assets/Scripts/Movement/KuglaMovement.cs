using Assets.Scripts.Instantiate;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

namespace Assets.Scripts.Movement
{
    public class KuglaMovement : MonoBehaviour
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
        private GameObject Terrain;

        [SerializeField]
        private GameObject WallInstantiateZoneRight;

        [SerializeField]
        private GameObject WallInstantiateZoneLeft;

        private Slider SpeedSlider;

        private Vector3 LastKuglaPosition;

        public static float ForwardSpeed { get; private set; }
        public static float SideSpeed { get; private set; }

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
            SideSpeed = PlayerPrefs.GetFloat("SideSpeed");
            ForwardSpeed = PlayerPrefs.GetFloat("ForwardSpeed");

            kugla.AddForce(2 * ForwardSpeed, 0, 5, ForceMode.VelocityChange);
            GameManager = FindObjectOfType<GameManager>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.tag == Constants.WallTag || collision.collider.tag==Constants.SuperWallTag)
            {
                if (LeftSide)
                    GoRight(kugla);
                else
                    GoLeft(kugla);
                LeftSide = !LeftSide;

                BallBounceAudio.Play();
                if (InstantiateWall.WallsRemained == 0)
                    GameManager.GameOver();
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

            if (kugla.transform.position.x % 20 < 1)
            {
                var vectorToAdd = new Vector3(20, 0, 0);
                Terrain.transform.localScale += vectorToAdd;
                WallInstantiateZoneLeft.transform.localScale += vectorToAdd;
                WallInstantiateZoneRight.transform.localScale += vectorToAdd;
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