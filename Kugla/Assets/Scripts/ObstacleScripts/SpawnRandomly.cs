using Assets.Scripts.ObstacleScripts.Pool;
using Assets.Scripts.Score;
using UnityEngine;

namespace Assets.Scripts.ObstacleScripts
{
    public class SpawnRandomly : MonoBehaviour
    {
        #pragma warning disable 0649
        [SerializeField]
        private GameObject kugla;

        [SerializeField]
        private float delay = 5f;

        private float lastTime;
        private float lastTimeSuperObstacle;

        #pragma warning restore 0649

        private void Start()
        {
            lastTime = 0;
            lastTimeSuperObstacle = 0;
        }
        private void Update()
        {
            if (Time.time - lastTime > delay)
            {
                SpawnObstacleFromPool();
            }
        }

        private void FixedUpdate()
        {
            if (ScoreData.Score%200<10 && Time.time-lastTimeSuperObstacle>3f)
            {
                SpawnObstacleFromPool(true);
                lastTimeSuperObstacle = Time.time;
            }
        }
        private void SpawnObstacleFromPool(bool superObstacle=false)
        {
            lastTime = Time.time;
            Vector3 position = new Vector3(Random.Range(kugla.transform.position.x + 20, kugla.transform.position.x + (superObstacle?30:50)), 0.5f, Random.Range(1.3f, 4.8f));
            var obstacle = BasicPool.Instance.GetFromPool(superObstacle);
            obstacle.transform.position = position;
        }
    }
}