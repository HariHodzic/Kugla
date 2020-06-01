using Assets.Scripts.ObstacleScripts.Pool;
using UnityEngine;

namespace Assets.Scripts.ObstacleScripts
{
    public class ObstacleCollision : MonoBehaviour
    {
        [SerializeField]
        private AudioSource SuperObstacleAudio;
    
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == Constants.KuglaTag)
            {
                BasicPool.Instance.AddToPool(gameObject);
            }

        }

    }
}
