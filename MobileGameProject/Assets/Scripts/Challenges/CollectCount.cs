using System;
using UnityEngine;

namespace Collectibles
{
    public class CollectCount : MonoBehaviour
    {
        public static event Action OnCollected;
        public static int total = 0;

        private void Awake() => total++;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                OnCollected?.Invoke();
                Destroy(gameObject);
            }
        }

    }
}
