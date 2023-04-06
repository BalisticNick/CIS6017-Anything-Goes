using System;
using UnityEngine;

namespace Collectibles
{
    public class CoinTracker : MonoBehaviour
    {
        public static event Action onCollected;
        public static int total = 0;

        private void Awake() => total++;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                onCollected?.Invoke();
                gameObject.SetActive(false);

            }
        }

    }
}
