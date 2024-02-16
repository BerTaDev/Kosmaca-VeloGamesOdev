using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BerTaDEV
{
    public class TilePrefab : MonoBehaviour
    {
        public Transform[] points;
        public GameObject[] prefabs;
        [ReadOnlyInspector] public List<GameObject> spawnedObjects = new List<GameObject>();

        public void GenerateTileFromEditor()
        {
            if (spawnedObjects.Count > 0)
            {
                for (int i = 0; i < spawnedObjects.Count; i++)
                {
                    DestroyImmediate(spawnedObjects[i]);
                }
            }

            spawnedObjects.Clear();
            for (int i = 0; i < points.Length; i++)
            {
                GameObject newob = Instantiate(prefabs[Random.Range(0, prefabs.Length)].gameObject, points[i].transform);
                newob.transform.localRotation = Quaternion.Euler(0.0f, Random.Range(0.0f, 360.0f), 0.0f);
                spawnedObjects.Add(newob);
            }
        }
        public void GenerateTile()
        {
            if (spawnedObjects.Count > 0)
            {
                for (int i = 0; i < spawnedObjects.Count; i++)
                {
                    Destroy(spawnedObjects[i]);
                }
            }
            
            spawnedObjects.Clear();
            for (int i = 0; i < points.Length; i++)
            {
                GameObject newob = Instantiate(prefabs[Random.Range(0, prefabs.Length)].gameObject, points[i].transform);
                newob.transform.localRotation = Quaternion.Euler(0.0f, Random.Range(0.0f, 360.0f), 0.0f);
                spawnedObjects.Add(newob);
            }
        }
    }
}
