using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using SceneSystem;
using Datapack;
using System;

namespace Collectibles
{
    public class CollectibleManager : MonoBehaviour
    {
        public static event Action onComplete;

        #region Varibles
        [SerializeField] private TextMeshProUGUI counterText;
        public static int counter = 0;
        public static bool isOpen = false;

        [SerializeField] private GameObject gate;

        #endregion

        #region Coin Collection
        void Start()
        {
            isOpen = false;

        }

        void OnEnable() => CoinTracker.onCollected += OnCollectibleCollected;

        void OnDisable() => CoinTracker.onCollected -= OnCollectibleCollected;

        void OnCollectibleCollected()
        {
            counter++; 
        }

        void UpdateCount()
        {

            if (counter == CoinTracker.total)
            {
                gate.GetComponent<Renderer>().material.color = Color.green;
                counterText.text = "Gate is Ready";

                onComplete?.Invoke();
            }
            else
            {
                counterText.text = $"{counter} / {CoinTracker.total}";
            }
        }
        #endregion

        private void Update()
        {
            UpdateCount();
        }
    }
}
