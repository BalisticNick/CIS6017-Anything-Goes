using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Datapack
{
    public class DatapackController : MonoBehaviour
    {
        [SerializeField] private GameObject overHeadText;

        public static bool canInteract = false;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                overHeadText.SetActive(true);
                canInteract = true;

                Debug.Log(canInteract);
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