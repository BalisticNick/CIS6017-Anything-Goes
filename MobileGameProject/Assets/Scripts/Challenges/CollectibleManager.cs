using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Collectibles
{

    public class CollectibleManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI counterText;
        private int counter = 0;
        private bool isOpen;

        void Start() => UpdateCount();

        void OnEnable() => CollectCount.OnCollected += OnCollectibleCollected;

        void OnDisable() => CollectCount.OnCollected -= OnCollectibleCollected;

        void OnCollectibleCollected()
        {
            counter++;
            UpdateCount();
        }

        void UpdateCount()
        {
            counterText.text = $"{counter} / {CollectCount.total}";
        }

        private void OnTriggerStay(Collider other)
        {
            if (counter == CollectCount.total)
            {

            }
        }
    }
}
