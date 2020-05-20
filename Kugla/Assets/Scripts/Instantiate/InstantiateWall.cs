using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Instantiate
{
    public class InstantiateWall : MonoBehaviour
    {
#pragma warning disable 0649

        [SerializeField]
        private GameObject BasicWall;

        [SerializeField]
        private GameObject SuperWall;
        [SerializeField]
        private AudioSource SuperWallStartAudio;

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

        public static bool SuperWallEnabled { get; private set; }
        private int SuperWallsRemained = 0;
        private float superWallTargetTime = 5.0f;
        private int basicWallsCounter = 0;

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
        }

        private void Update()
        {
            superWallTargetTime -= Time.deltaTime;

            if (WallsRemained > 0)
            {
                if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    //if (Input.GetMouseButtonDown(0))
                    //{
                    //Mobile touch
                    Vector3 fingerPos = Input.GetTouch(0).position;

                    var ray = Camera.main.ScreenPointToRay(fingerPos);

                    if (Physics.Raycast(ray, out RaycastHit hit))
                    {
                        if (LastWallPoint != Vector3.zero &&
                            !(LeftSideTerrainCondition(hit.point) || RightSideTerrainCondition(hit.point)))
                            return;

                        WallsDecrement();

                        Instantiate(SuperWallEnabled ? SuperWall : BasicWall, new Vector3(hit.point.x, hit.point.y + 0.2f, hit.point.z),
                            Quaternion.identity);

                        WallInstantiateAudio.Play();
                        basicWallsCounter++;
                        LastWallPoint = hit.point;

                        if (basicWallsCounter == 3 && superWallTargetTime >= 0)
                        {
                            SuperWallEnabled = true;
                            SuperWallsRemained = 5;

                            //Added 5 new basic walls on every super wall enabling
                            WallsRemainedTemp = WallsRemained+5;
                            WallsRemained = SuperWallsRemained;
                            WallsRemainedLabel.color = Color.yellow;
                            WallsRemainedLabel.alignment = TextAnchor.MiddleLeft;
                            WallsRemainedValue.alignment = TextAnchor.MiddleCenter;
                            WallsRemainedValue.color = Color.yellow;
                            WallsRemainedLabel.text = "Super walls:";
                            SuperWallStartAudio.Play();
                        }

                        if (SuperWallEnabled && WallsRemained == 0)
                        {
                            WallsRemained = WallsRemainedTemp;
                            SuperWallEnabled = false;
                            superWallTargetTime = 5.0f;
                            basicWallsCounter = 0;
                            WallsRemainedLabel.color = Color.black;
                            WallsRemainedLabel.alignment = TextAnchor.MiddleCenter;
                            WallsRemainedValue.alignment = TextAnchor.MiddleLeft;
                            WallsRemainedValue.color = Color.black;
                            WallsRemainedLabel.text = "Walls";
                        }
                    }
                }
                //}
            }
        }
    }
}