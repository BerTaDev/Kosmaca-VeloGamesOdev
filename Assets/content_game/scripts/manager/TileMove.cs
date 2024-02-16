using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BerTaDEV
{
    public class TileMove : MonoBehaviour
    {
        public float speed;
        TilePrefab[] tilePrefabs;

        private void Start()
        {
            tilePrefabs = GetComponentsInChildren<TilePrefab>();
            ReGenerateTiles();
        }
        public float ActiveSpeed() { return speed * GameManager.singleton.game_speed; }
        private void Update()
        {
            if (!GameManager.singleton.isGame) return;
            transform.Translate(-transform.forward * ActiveSpeed() * Time.deltaTime);
        }
        public void onTriggerEnterCharacter()
        {
            transform.position = new Vector3(0, 0, 36);
            ReGenerateTiles();
        }
        void ReGenerateTiles()
        {
            for (int i = 0; i < tilePrefabs.Length; i++)
            {
                tilePrefabs[i].GenerateTile();
            }
        }
    }
}
