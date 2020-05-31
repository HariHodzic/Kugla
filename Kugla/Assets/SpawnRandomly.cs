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
        Vector3 position = new Vector3(kugla.transform.position.x+20,1,kugla.transform.position.z);
        var obstacle = BasicPool.Instance.GetFromPool();
        obstacle.transform.position = position;
    }
}
