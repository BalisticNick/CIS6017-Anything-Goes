using Collectibles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SceneSystem;

public class DoorExit : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        SceneDirector.WinGame();
    }
}
