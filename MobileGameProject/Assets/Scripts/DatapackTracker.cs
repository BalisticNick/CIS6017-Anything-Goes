using Collectibles;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Datapack
{
    public class DatapackTracker : MonoBehaviour
    {
        [SerializeField] private GameObject overHeadText;

        public static int total;
        public static bool canInteract = false;

        void Start() => total++;

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