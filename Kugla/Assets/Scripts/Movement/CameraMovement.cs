using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private Transform kugla;
    [SerializeField]
    private float smoothSpeed=10.0f;
    
    void FixedUpdate()
    {
        Vector3 desiredPosition = new Vector3(kugla.position.x-1,13,0);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
    }
}
