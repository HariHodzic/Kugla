using UnityEngine;

namespace Assets.Scripts.Movement
{
    public class CameraMovement : MonoBehaviour
    {
        #pragma warning disable 0649
        [SerializeField]
        private Transform kugla;

        [SerializeField]
        private float smoothSpeed=10.0f;
        #pragma warning restore 0649


        void FixedUpdate()
        {
            Vector3 desiredPosition = new Vector3(kugla.position.x-1,12.5f,3.5f);
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
            transform.position = smoothedPosition;
        }
    }
}
