using Collectibles;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Datapack
{

    public class DatapackManager : MonoBehaviour
    {

        public TextMeshProUGUI counterText;

        public static int datapack = 0;
        public static bool HasCompleted = false;

        #region datapack Collection


        void OnEnable() => CollectibleManager.onComplete += onCompelete;

        void OnDisable() => CollectibleManager.onComplete -= onCompelete;

        void onCompelete()
        {
            if (HasCompleted == true)
            {
                datapack++;
            }
        }

        public void UpdateCount()
        {
            counterText.text = $"{datapack} / {DatapackTracker.total}";
        }

        private void Start()
        {
              UpdateCount();
        }
        #endregion



    }
}
