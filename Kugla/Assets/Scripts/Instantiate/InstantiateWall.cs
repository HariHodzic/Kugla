using UnityEngine;
using UnityEngine.Analytics;

namespace Assets.Scripts.Instantiate
{
    public class InstantiateWall : MonoBehaviour
    {
#pragma warning disable 0649

        [SerializeField]
        private GameObject BasicWall;

        [SerializeField]
        private Terrain Terrain;

        public static bool WallExists = false;
        private Vector3 LastWallPoint = Vector3.zero;

        private bool LeftSideTerrainCondition(Vector3 point) => point.x - LastWallPoint.x > 4 && point.z >= 6 && point.z <= 7;

        private bool RightSideTerrainCondition(Vector3 point) => point.x - LastWallPoint.x > 4 && point.z >= 0.1 && point.z <= 1;

#pragma warning restore 0649

        private void Update()
        {
            //if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            if (Input.GetMouseButtonDown(0))
            {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out RaycastHit hit) && (LeftSideTerrainCondition(hit.point) || RightSideTerrainCondition(hit.point)))
                {
                    //Vector3 fingerPos = Input.mousePosition;
                    //fingerPos.z = 10f;
                    //Vector3 touchPos = Camera.main.ScreenToWorldPoint(fingerPos);
                    //var touchPos = Input.mousePosition;
                    //touchPos.y = 1f;

                    Instantiate(BasicWall, new Vector3(hit.point.x, hit.point.y + 0.2f, hit.point.z), Quaternion.identity);
                    LastWallPoint = hit.point;
                    WallExists = true;
                }
                //Instantiate(BasicWall, touchPos, Quaternion.identity);
            }
        }
    }
}