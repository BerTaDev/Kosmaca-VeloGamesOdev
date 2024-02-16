using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BerTaDEV
{
    public class ObjectPooling : MonoBehaviour
    {
        public static ObjectPooling singleton;
        public List<GameObject> poolObjects = new List<GameObject>();
        [Space]
        [SerializeField] GameObject[] obstacles;
        public int amount = 10;
        GameObject beforeobjecT;
        private void Awake()
        {
            singleton = this;
        }

        private void Start()
        {
            for (int i = 0; i < amount; i++)
            {
                GameObject obj = Instantiate(obstacles[Random.Range(0, obstacles.Length)], transform);
                obj.transform.localPosition = Vector3.zero;
                obj.SetActive(false);
                poolObjects.Add(obj);
            }
        }

        public GameObject GetPooledObject()
        {
            for (int i = 0; i < poolObjects.Count; i++)
            {
                if (!poolObjects[i].activeInHierarchy && poolObjects[i] != beforeobjecT)
                {
                    beforeobjecT = poolObjects[i];
                    return poolObjects[i];
                }
            }
            return null;
        }
        public void DisableAllObstacles()
        {
            for (int i = 0; i < poolObjects.Count; i++)
            {
                beforeobjecT = null;
                poolObjects[i].SetActive(false);
            }
        }
    }
}
