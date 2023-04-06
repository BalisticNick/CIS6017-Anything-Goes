using Collectibles;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Datapack
{
    public class DatapackManager : MonoBehaviour
    {
        [SerializeField] private GameObject overHeadText;

        int total;
        public static bool canInteract = false;
        public static bool HasCompleted = false;

        private void Awake()
        {
            total = DatapackTracker.datapack;
        }

        void OnEnable() => DatapackTracker.onCompleted += onDatapackComplete;

        void OnDisable() => DatapackTracker.onCompleted -= onDatapackComplete;

        public void onDatapackComplete()
        {
            Destroy(gameObject);

            total = DatapackTracker.datapack;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                overHeadText.SetActive(true);
                canInteract = true;

                Debug.Log(canInteract);
            }
            else
            {
                canInteract = false;
            }
        }

            private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                overHeadText.SetActive(false);
                canInteract = false;
                Debug.Log(canInteract);
            }
        }

        
    }
}