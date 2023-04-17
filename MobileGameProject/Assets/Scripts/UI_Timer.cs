using SceneSystem;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Timer : MonoBehaviour
{
    public float totalTimeAllowed;
    public float currentTime;
    public float timeRemaining;
    public bool usedExtraLife { get; set; }
    [SerializeField] private TextMeshProUGUI TimerText;
    [SerializeField] private GameObject ContinueBox;
    private void Awake()
    {
        totalTimeAllowed = 120f;
        currentTime = 0f;
        usedExtraLife = false;
    }

    private void Update()
    {
        if (currentTime <= totalTimeAllowed)
            currentTime += Time.deltaTime;

        currentTime = Mathf.Clamp(currentTime, 0, totalTimeAllowed);

        timeRemaining = totalTimeAllowed - currentTime;
        string minutes = Mathf.Floor(timeRemaining / 60).ToString("0");
        string seconds = Mathf.Floor(timeRemaining % 60).ToString("00");

        TimerText.text = minutes + ":" + seconds;

        if (currentTime >= totalTimeAllowed)
        {
            if (!usedExtraLife)
            {
                ContinueBox.SetActive(true);
                Time.timeScale = 0;
            }
            else
                //invoke death screen
                SceneDirector.LoseGame("Oh dear, time has run out!");
        }
        else
        {
            ContinueBox.SetActive(false);
            Time.timeScale = 1;
        }
    }

    
}
