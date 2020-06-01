using Assets.Scripts.Movement;
using Assets.Scripts.Score;
using Assets.Scripts.UI.MainManu;
using UnityEngine;
using UnityEngine.UI;
using Vector3 = UnityEngine.Vector3;

namespace Assets.Scripts.WallScripts
{
    //ScoreData inherits MonoBehaviour
    public class InstantiateWall : ScoreData
    {
#pragma warning disable 0649

        //BasicWall
        [SerializeField]
        private GameObject BasicWall;


        //SuperWall
        [SerializeField]
        private GameObject SuperWall;

        [SerializeField]
        private AudioSource SuperWallStartAudio;

        [SerializeField]
        private GameObject SuperWallGoalBar;

        [SerializeField]
        private Image SuperWallGoalBarContainer;

        private float SuperWallGoalBarHeight;

        public static bool SuperWallEnabled { get; private set; }

        //4 seconds for 5 basic walls to reach super walls
        private float superWallTargetTime = 4.0f;

        [SerializeField]
        private Terrain Terrain;

        [SerializeField]
        private AudioSource WallInstantiateAudio;

        private Text WallsRemainedLabel;
        private Text WallsRemainedValue;

        public static bool WallExists { get; set; } = false;

        private Vector3 LastWallPoint = Vector3.zero;

        private int WallsRemainedTemp;

        public static void WallsDecrement()
        {
            if (SuperWallEnabled)
                --SuperWallsRemained;
            else
                --BasicWallsRemained;
        }

        //LEFT WALL DISTANCE CONDITION
        //((Math.Abs(LastWallPoint.z - point.z) > 1.5) || point.x - LastWallPoint.x > 3)
        private bool LeftSideTerrainCondition(Vector3 point) => point.z >= 5.5 && point.z <= 7;

        //RIGHT WALL DISTANCE CONDITION
        //((Math.Abs(LastWallPoint.z - point.z) > 1.5) || point.x - LastWallPoint.x > 3)
        private bool RightSideTerrainCondition(Vector3 point) => point.z >= 0.1 && point.z <= 1.5;

#pragma warning restore 0649

        private void Start()
        {
            BasicWallsRemained = 10;
            SuperWallEnabled = false;
            WallsRemainedLabel = GameObject.Find(Constants.WallsRemainedLabel).GetComponent<Text>();
            WallsRemainedValue = GameObject.Find(Constants.WallsRemainedValue).GetComponent<Text>();
            SuperWallGoalBarHeight = SuperWallGoalBar.transform.localScale.y;
            SuperWallGoalBar.transform.localScale -= new Vector3(0, SuperWallGoalBar.transform.localScale.y, 0);
        }

        private void Update()
        {
            superWallTargetTime -= Time.deltaTime;

            if (BasicWallsRemained > 0 || SuperWallsRemained>0)
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

                        AddScore(SuperWallEnabled ? 5 : 2);

                        WallInstantiateAudio.Play();
                        if (!SuperWallEnabled)
                        {
                            SuperWallGoalBar.transform.localScale += new Vector3(0, SuperWallGoalBarHeight / 5, 0);
                        }

                        LastWallPoint = hit.point;

                        if (KuglaMovement.BasicBounces == 5)
                        {
                            if (superWallTargetTime >= 0)
                                StartSuperWalls();
                            else
                            {
                                superWallTargetTime = 5f;
                                SuperWallGoalBar.transform.localScale -= new Vector3(0, SuperWallGoalBar.transform.localScale.y, 0);
                            }
                        }

                        if (SuperWallEnabled && SuperWallsRemained == 0)
                        {
                            StartBasicWalls();
                        }
                    }
                }
            }
            else
            {
                GameManager.GameOver();
            }
        }

        private void StartSuperWalls()
        {
            if(BasicWallsRemained==0)
                GameManager.GameOver();

            SuperWallEnabled = true;
            SuperWallsRemained += 5;

            //Add 2 new basic walls on every super wall enabling
            BasicWallsRemained += 2;
            WallsRemainedLabel.alignment = TextAnchor.MiddleLeft;
            WallsRemainedValue.alignment = TextAnchor.MiddleCenter;
            WallsRemainedValue.font.material.color = Constants.DarkGoldColor;
            WallsRemainedLabel.font.material.color = Constants.DarkGoldColor;
            WallsRemainedLabel.text = "Super walls:";

            SuperWallGoalBarContainer.enabled = false;
            SuperWallGoalBar.transform.localScale -= new Vector3(0, SuperWallGoalBar.transform.localScale.y, 0);
            SuperWallStartAudio.Play();
        }

        private void StartBasicWalls()
        {
            if(BasicWallsRemained==0)
                GameManager.GameOver();

            SuperWallEnabled = false;
            SuperWallsRemained = 0;
            superWallTargetTime = 5.0f;
            WallsRemainedLabel.alignment = TextAnchor.MiddleCenter;
            WallsRemainedValue.alignment = TextAnchor.MiddleLeft;
            WallsRemainedValue.font.material.color = Constants.FineBlackColor;
            WallsRemainedLabel.font.material.color = Constants.FineBlackColor;
            WallsRemainedLabel.text = "Walls";
            SuperWallGoalBarContainer.enabled = true;
        }
    }
}