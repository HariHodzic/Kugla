using Assets.Scripts.Movement;
using Assets.Scripts.Score;
using Assets.Scripts.UI.MainManu;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace Assets.Scripts.WallScripts
{
    //ScoreData inherits MonoBehaviour
    public class InstantiateWall : ScoreData
    {
#pragma warning disable 0649

        //BasicWall
        [SerializeField] private GameObject BasicWall;

        //SuperWall
        [SerializeField] private GameObject SuperWall;

        [SerializeField] private Terrain Terrain;

        [SerializeField] private AudioSource WallInstantiateAudio;

        public static bool WallExists { get; set; } = false;

        private Vector3 LastWallPoint = Vector3.zero;

        private int WallsRemainedTemp;

        public static void WallsDecrement()
        {
            if (KuglaMovement.SuperWallsEnabled)
                --SuperWallsRemained;
            else
                --BasicWallsRemained;
        }

        private bool SuperWallsStarted;

        //LEFT WALL DISTANCE CONDITION
        //((Math.Abs(LastWallPoint.z - point.z) > 1.5) || point.x - LastWallPoint.x > 3)
        private bool LeftSideTerrainCondition(Vector3 point) => point.z >= 5.5 && point.z <= 7;

        //RIGHT WALL DISTANCE CONDITION
        //((Math.Abs(LastWallPoint.z - point.z) > 1.5) || point.x - LastWallPoint.x > 3)
        private bool RightSideTerrainCondition(Vector3 point) => point.z >= 0.1 && point.z <= 1.5;

#pragma warning restore 0649

        private void Start()
        {
            SuperWallsStarted = false;
            BasicWallsRemained = 20;
            SuperWallsRemained = 0;
        }

        private void Update()
        {
            if (BasicWallsRemained > 0 || SuperWallsRemained > 0)
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

                        Instantiate(KuglaMovement.SuperWallsEnabled ? SuperWall : BasicWall,
                            new Vector3(hit.point.x, hit.point.y + 0.2f, hit.point.z),
                            Quaternion.identity);

                        AddScore(KuglaMovement.SuperWallsEnabled ? 5 : 2);

                        WallInstantiateAudio.Play();

                        LastWallPoint = hit.point;

                        if (!SuperWallsStarted && KuglaMovement.SuperWallsEnabled)
                            StartSuperWalls();
                    }

                    if (KuglaMovement.SuperWallsEnabled && SuperWallsRemained == 0)
                    {
                        StartBasicWalls();
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
            if (SuperWallsRemained == 0)
                return;

            SuperWallsStarted = true;
            SuperWallsRemained += 5;

            //Add 2 new basic walls on every super wall enabling
            BasicWallsRemained += 5;
        }

        private void StartBasicWalls()
        {
            if (BasicWallsRemained == 0)
                GameManager.GameOver();
            SuperWallsStarted = false;

            SuperWallsRemained = 0;
        }
    }
}