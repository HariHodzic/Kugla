using Assets.Scripts.Instantiate;
using Unity.Collections;
using UnityEngine;

namespace Assets.Scripts.WallScripts
{
    public class WallBehaviour : MonoBehaviour
    {
        [ReadOnly]
        private bool IsActive;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag(Constants.KuglaTag))
            {
                Destroy(this.gameObject);
                InstantiateWall.WallExists = false;
            }
        }
    }
}
