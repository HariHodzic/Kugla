using UnityEngine;
using UnityEngine.Experimental.UIElements;
using Slider = Assets.Scripts.UI.OptionsMenu.Slider;

namespace Assets.Scripts.Movement
{
    public class KuglaMovement : MonoBehaviour
    {
        private PlayerPrefs PlayerPrefs;

        [SerializeField]
        private Rigidbody kugla;

        private bool LeftSide = true;

        private GameObject Terrain;

        private Slider SpeedSlider;

        private void GoLeft(Rigidbody rb)
        {
            rb.AddForce(PlayerPrefs.GetFloat("ForwardSpeed"), 0, PlayerPrefs.GetFloat("SideSpeed"), ForceMode.VelocityChange);
        }

        private void GoRight(Rigidbody rb)
        {
            rb.AddForce(PlayerPrefs.GetFloat("ForwardSpeed"), 0, -PlayerPrefs.GetFloat("SideSpeed"), ForceMode.VelocityChange);
        }

        private void Start()
        {
            var msg = $"Side speed -> {PlayerPrefs.GetFloat("SideSpeed")}";
            Debug.Log(msg);
            //SpeedSlider = GameObject.Find("ForwardSpeedSlider").GetComponent<Slider>();
            PlayerPrefs.SetFloat("ForwardSpeed", 3);
            //PlayerPrefs.SetFloat("SideSpeed", 3);
            PlayerPrefs.Save();

            //SpeedSlider.value = 3;

            Rigidbody kugla = GetComponent<Rigidbody>();
            GoLeft(kugla);
            Terrain = GameObject.FindGameObjectWithTag(Constants.TerrainTag);
        }

        private void OnCollisionEnter(Collision collision)
        {
            string wallTag = Constants.WallTag;
            if (collision.collider.tag == wallTag)
            {
                Rigidbody kugla = GetComponent<Rigidbody>();
                if (LeftSide)
                    GoRight(kugla);
                else
                    GoLeft(kugla);
                LeftSide = !LeftSide;
            }
        }

        private void FixedUpdate()
        {
            var kugla = GetComponent<Rigidbody>();
            //if (LeftSide)
            //    GoLeft(kugla);
            //else
            //    GoRight(kugla);
            if (kugla.transform.position.y < 0)
            {
                FindObjectOfType<GameManager>().GameOver();
            }

            if (kugla.transform.position.x % 20 < 1)
            {
                Terrain.transform.localScale += new Vector3(20, 0, 0);
            }
        }

        public void AdjustSideSpeed(float speed)
        {
            PlayerPrefs.SetFloat("SideSpeed",speed);
            PlayerPrefs.Save();
        }

        public void AdjustForwardSpeed(float speed)
        {
            PlayerPrefs.SetFloat("ForwardSpeed",speed);
            PlayerPrefs.Save();
        }
    }
}