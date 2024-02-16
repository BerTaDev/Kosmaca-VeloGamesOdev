using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BerTaDEV
{
    public class ObstacleTile : MonoBehaviour
    {
        public float speed;
        public float ActiveSpeed() { return speed * GameManager.singleton.game_speed; }
        [ReadOnlyInspector] public List<GameObject> myCoins;
        private void Start()
        {
            Transform[] myObject = GetComponentsInChildren<Transform>();
            foreach (var item in myObject)
            {
                if (item.CompareTag("Coin"))
                    myCoins.Add(item.gameObject);
            }
        }
        private void Update()
        {
            if (!GameManager.singleton.isGame) return;

            transform.Translate(-Vector3.forward * ActiveSpeed() * Time.deltaTime);
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("EndOfTheWorld"))
            {
                onEnd();
            }
        }
        private void onEnd()
        {
            foreach (var item in myCoins)
            {
                item.SetActive(true);
            }
            gameObject.SetActive(false);
        }
    }
}
