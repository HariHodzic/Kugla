using UnityEngine;

namespace Assets.Scripts.Instantiate
{
    public class InstantiateWall : MonoBehaviour
    {
        [SerializeField]
        private GameObject BasicWall;

        private bool WallExists = false;

        private void FixedUpdate()
        {
            //if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 fingerPos = Input.mousePosition;
                fingerPos.z = 10f;
                //Vector3 touchPos = Camera.main.ScreenToWorldPoint(fingerPos);
                var touchPos = Input.mousePosition;
                touchPos.y = 1f;
                var ray=Camera.main.ScreenPointToRay(Input.mousePosition);
     
                if(Physics.Raycast(ray,out RaycastHit hit))
                {
         
                    if(Input.GetKey(KeyCode.Mouse0))
                    {
                       Instantiate(BasicWall,new Vector3(hit.point.x,hit.point.y+1,hit.point.z), Quaternion.identity);
             
                    }
         
                }
                //Instantiate(BasicWall, touchPos, Quaternion.identity);
                WallExists = true;
            }
        }
    }
}