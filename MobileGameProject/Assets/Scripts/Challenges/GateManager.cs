using Collectibles;
using UnityEngine;

public class GateManager : MonoBehaviour
{

    private void OnTriggerStay(Collider other)
    {
        if (CollectibleManager.counter == CoinTracker.total)
        {
            CollectibleManager.isOpen = true;
        }
        else
        {
            CollectibleManager.isOpen = false;
        }
    }
}
