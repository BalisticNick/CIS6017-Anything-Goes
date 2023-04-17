using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPlayerPrefs : MonoBehaviour
{
    //Clear PlayerPrefs when triggered (OnClick)
    public void OnClick()
    {
        PlayerPrefs.DeleteAll();
    }
}
