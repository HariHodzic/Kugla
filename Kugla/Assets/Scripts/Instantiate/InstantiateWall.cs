using System;
using UnityEngine;

namespace Assets.Scripts.Instantiate
{
    public class InstantiateWall : MonoBehaviour
    {
#pragma warning disable 0649

        [SerializeField] private GameObject BasicWall;

        [SerializeField] private Terrain Terrain;

        public static bool WallExists = false;
        private Vector3 LastWallPoint = Vector3.zero;

        private bool LeftSideTerrainCondition(Vector3 point) =>
            ((Math.Abs(LastWallPoint.z - point.z) > 1.5) || point.x - LastWallPoint.x > 3) && point.z >= 5.5 && point.z <= 7;

        private bool RightSideTerrainCondition(Vector3 point) =>
            ((Math.Abs(LastWallPoint.z - point.z) > 1.5) || point.x - LastWallPoint.x > 3) && point.z >= 0.1 && point.z <= 1.5;

#pragma warning restore 0649

        private void Start()
        {
            Terrain = GameObject.FindGameObjectWithTag(Constants.TerrainTag).GetComponent<Terrain>();
        }

        private void Update()
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
                    Instantiate(BasicWall, new Vector3(hit.point.x, hit.point.y + 0.2f, hit.point.z),
                        Quaternion.identity);
                    LastWallPoint = hit.point;
                }
            }
            //}
        }
    }
}