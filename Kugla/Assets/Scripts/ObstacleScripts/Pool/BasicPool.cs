using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.ObstacleScripts.Pool
{
    public class BasicPool : MonoBehaviour
    {
        [SerializeField]
        private GameObject prefab;

        [SerializeField]
        private GameObject SuperObstaclePrefab;

        private Queue<GameObject> availableObjects = new Queue<GameObject>();
        public static BasicPool Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
            GrowPool();
        }

        public GameObject GetFromPool(bool superObstacle = false)
        {
            if (availableObjects.Count == 0)
                GrowPool(superObstacle);
            else if (superObstacle)
            {
                GrowPool(true);
            }
            var instance = availableObjects.Dequeue();
            instance.SetActive(true);
            return instance;
        }

        private void GrowPool(bool superObstacle = false)
        {
            var instanceToAdd = Instantiate(superObstacle ? SuperObstaclePrefab : prefab);
            instanceToAdd.transform.SetParent(transform);
            if (superObstacle)
            {
                availableObjects.Clear();
            }
            AddToPool(instanceToAdd);
        }

        public void AddToPool(GameObject instance)
        {
            instance.SetActive(false);
            availableObjects.Enqueue(instance);
        }
    }
}