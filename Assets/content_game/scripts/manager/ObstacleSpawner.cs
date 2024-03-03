using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BerTaDEV
{
    public class ObstacleSpawner : MonoBehaviour
    {
        public ObjectPooling obstacleTile;
        public Transform obstaclePoint;
        [Space]
        public float delay = 5;
        float timer;
        public static ObstacleSpawner singleton;
        private void Awake()
        {
            singleton = this;
        }
        private void Update()
        {
            if (GameManager.singleton.isGame)
            {
                timer += Time.deltaTime;
                if (timer >= delay)
                {
                    timer = 0;
                    spawnObstacleTile();
                }
            }
        }
        void spawnObstacleTile()
        {
            GameObject obj = obstacleTile.GetRandomPooledObject();
            obj.SetActive(true);
            obj.transform.position = obstaclePoint.position;

        }
        public void onNewGame()
        {
            timer = 0;
        }
    }
}
