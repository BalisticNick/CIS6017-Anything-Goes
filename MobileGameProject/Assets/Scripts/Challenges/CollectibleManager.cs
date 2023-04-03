using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using SceneSystem;

namespace Collectibles
{

    public class CollectibleManager : MonoBehaviour
    {
        #region Varibles
        [SerializeField] private TextMeshProUGUI counterText;
        private int counter = 0;
        private bool isOpen;
        #endregion

        #region Coin Collection
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
        #endregion

        private void OnTriggerStay(Collider other)
        {
            if (counter == CollectCount.total)
            {
                SceneDirector.StartGame();
            }
        }
    }
}
