using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    //Setup references
    [SerializeField] private GameObject ContinueBox;
    [SerializeField] private UI_Timer TimerUI;

    private void Awake()
    {
        //Singleton, only one instance of GameManager can exist.
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        //Grab references if null
        if (ContinueBox == null)
            ContinueBox = GameObject.Find("ContinueBox");
        if (TimerUI == null)
            TimerUI = GameObject.Find("TimerUI").GetComponent<UI_Timer>();
    }
    //Pause Game
    public static void PauseGame()
    {
        Time.timeScale = 0;
    }
    //Resume Game
    public static void ResumeGame()
    {
        Time.timeScale = 1;
    }

    //Getters
    private GameObject GetContinueBox()
    {
        return ContinueBox;
    }
    public static GameObject GetContinueBox_Static()
    {
        return Instance.GetContinueBox();
    }
    private UI_Timer GetTimerUI()
    {
        return TimerUI;
    }
    public static UI_Timer GetTimerUI_Static()
    {
        return Instance.GetTimerUI();
    }
}
