using Collectibles;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Datapack
{

    public class DatapackTracker : MonoBehaviour
    {
        public static event Action onCompleted;

        [SerializeField] private TextMeshProUGUI counterText;  

        public static int datapack = 0;

        #region datapack Collection
        void Start() => UpdateCount();

        public bool onCompelete()
        {
            if (DatapackManager.HasCompleted == true)
            {
                onCompleted?.Invoke();
                datapack--;
            }
            return true;
        }

        void onDatapackComplete()
        {
            datapack++;
            UpdateCount();

        }

        void UpdateCount()
        {
            counterText.text = $"{datapack} / {datapack}";
        }
        #endregion



    }
}
