using Assets.Scripts.UI.MainManu;
using UnityEngine;
using UnityEngine.UI;
using Vector3 = UnityEngine.Vector3;

namespace Assets.Scripts.Instantiate
{
    public class InstantiateWall : MonoBehaviour
    {
#pragma warning disable 0649

        //BasicWall
        [SerializeField]
        private GameObject BasicWall;

        private int basicWallsCounter = 0;

        //SuperWall
        [SerializeField]
        private GameObject SuperWall;

        [SerializeField]
        private AudioSource SuperWallStartAudio;

        [SerializeField]
        private GameObject SuperWallGoalBar;

        [SerializeField]
        private Image SuperWallGoalBarContainer;

        private float SuperWallGoaldHeight;

        public static bool SuperWallEnabled { get; private set; }
        private int SuperWallsRemained = 0;
        private float superWallTargetTime = 5.0f;

        [SerializeField]
        private Terrain Terrain;

        [SerializeField]
        private AudioSource WallInstantiateAudio;

        private Text WallsRemainedLabel;
        private Text WallsRemainedValue;

        public static bool WallExists { get; set; } = false;

        private Vector3 LastWallPoint = Vector3.zero;

        public static int WallsRemained { get; private set; }
        private int WallsRemainedTemp;

        public static void WallsDecrement() => --WallsRemained;

        //LEFT WALL DISTANCE CONDITION
        //((Math.Abs(LastWallPoint.z - point.z) > 1.5) || point.x - LastWallPoint.x > 3)
        private bool LeftSideTerrainCondition(Vector3 point) => point.z >= 5.5 && point.z <= 7;

        //RIGHT WALL DISTANCE CONDITION
        //((Math.Abs(LastWallPoint.z - point.z) > 1.5) || point.x - LastWallPoint.x > 3)
        private bool RightSideTerrainCondition(Vector3 point) => point.z >= 0.1 && point.z <= 1.5;

#pragma warning restore 0649

        private void Start()
        {
            WallsRemained = 10;
            SuperWallEnabled = false;
            WallsRemainedLabel = GameObject.Find(Constants.WallsRemainedLabel).GetComponent<Text>();
            WallsRemainedValue = GameObject.Find(Constants.WallsRemainedValue).GetComponent<Text>();
            SuperWallGoaldHeight = SuperWallGoalBar.transform.localScale.y;
            SuperWallGoalBar.transform.localScale -= new Vector3(0, SuperWallGoalBar.transform.localScale.y, 0);

            
        }

        private void Update()
        {
            superWallTargetTime -= Time.deltaTime;

            if (WallsRemained > 0)
            {
                if (MainMenuScript.MobileMode == 0
                    ? Input.GetMouseButtonDown(0)
                    : Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    Ray ray;

                    if (MainMenuScript.MobileMode == 1)
                    {
                        var fingerPos = Input.GetTouch(0).position;
                        ray = Camera.main.ScreenPointToRay(fingerPos);
                    }
                    else
                    {
                        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    }

                    if (Physics.Raycast(ray, out RaycastHit hit))
                    {
                        if (LastWallPoint != Vector3.zero &&
                            !(LeftSideTerrainCondition(hit.point) || RightSideTerrainCondition(hit.point)))
                            return;

                        WallsDecrement();

                        Instantiate(SuperWallEnabled ? SuperWall : BasicWall, new Vector3(hit.point.x, hit.point.y + 0.2f, hit.point.z),
                            Quaternion.identity);

                        WallInstantiateAudio.Play();
                        if (!SuperWallEnabled)
                        {
                            basicWallsCounter++;
                            SuperWallGoalBar.transform.localScale += new Vector3(0, SuperWallGoaldHeight / 5, 0);
                        }

                        LastWallPoint = hit.point;

                        if (basicWallsCounter == 5 && superWallTargetTime >= 0)
                        {
                            SuperWallEnabled = true;
                            SuperWallsRemained = 5;
                            basicWallsCounter = 0;

                            //Added 5 new basic walls on every super wall enabling
                            WallsRemainedTemp = WallsRemained + 5;
                            WallsRemained = SuperWallsRemained;
                            WallsRemainedLabel.alignment = TextAnchor.MiddleLeft;
                            WallsRemainedValue.alignment = TextAnchor.MiddleCenter;
                            WallsRemainedValue.font.material.color = Constants.DarkGoldColor;
                            WallsRemainedLabel.font.material.color = Constants.DarkGoldColor;
                            WallsRemainedLabel.text = "Super walls:";

                            SuperWallGoalBarContainer.enabled = false;
                            SuperWallGoalBar.transform.localScale -= new Vector3(0, SuperWallGoalBar.transform.localScale.y, 0);
                            SuperWallStartAudio.Play();
                        }

                        if (SuperWallEnabled && WallsRemained == 0)
                        {
                            WallsRemained = WallsRemainedTemp;
                            SuperWallEnabled = false;
                            superWallTargetTime = 5.0f;
                            basicWallsCounter = 0;
                            WallsRemainedLabel.alignment = TextAnchor.MiddleCenter;
                            WallsRemainedValue.alignment = TextAnchor.MiddleLeft;
                            WallsRemainedValue.font.material.color = Constants.FineBlackColor;
                            WallsRemainedLabel.font.material.color = Constants.FineBlackColor;
                            WallsRemainedLabel.text = "Walls";
                            SuperWallGoalBarContainer.enabled = true;
                        }
                    }
                }
                //}
            }
        }
    }
}