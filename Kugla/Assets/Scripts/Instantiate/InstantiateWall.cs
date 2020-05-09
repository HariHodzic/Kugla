using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateWall : MonoBehaviour
{
    [SerializeField]
    private GameObject wall;
    private void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Vector3 fingerPos = Input.GetTouch(0).position;
            fingerPos.z = 10f;
            Vector3 touchPos = Camera.main.ScreenToWorldPoint(fingerPos);
            touchPos.y = 1f;
            Instantiate(wall, touchPos, Quaternion.identity);
        }
    }
}
