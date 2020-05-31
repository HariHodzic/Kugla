using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRandomly : MonoBehaviour
{
    [SerializeField]
    private GameObject kugla;
    [SerializeField]
    private float delay = 5f;
    [SerializeField]
    private GameObject obstaclePrefab;
    private float lastTime;
    private void Update()
    {
        if (Time.time - lastTime > delay)
        {
            SpawnObstacleFromPool();
        }
    }
    private void SpawnObstacleFromPool()
    {
        lastTime = Time.time;
        Vector3 position = new Vector3(Random.Range(kugla.transform.position.x+20, kugla.transform.position.x + 50), 0.5f, Random.Range(1.3f,4.8f));
        var obstacle = BasicPool.Instance.GetFromPool();
        obstacle.transform.position = position;
    }
}
