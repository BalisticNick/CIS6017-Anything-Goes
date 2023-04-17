using SceneSystem;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Timer : MonoBehaviour
{
    //Setup variables getters/setters
    public float totalTimeAllowed { get; set; }
    public float currentTime { get; private set; }
    public float timeRemaining { get; private set; }
    public bool usedExtraLife { get; set; }

    [SerializeField] private TextMeshProUGUI TimerText;
    private void Awake()
    {
        //Initialize variables
        totalTimeAllowed = 120f;
        currentTime = 0f;
        timeRemaining = 0f;
        usedExtraLife = false;
    }

    private void Update()
    {
        //Tick currentTime by deltaTime
        if (currentTime <= totalTimeAllowed)
            currentTime += Time.deltaTime;

        //Clamp currentTime value between 0 and totalTimeAllowed
        currentTime = Mathf.Clamp(currentTime, 0, totalTimeAllowed);

        //Calculate time remaining & format 0:00
        timeRemaining = totalTimeAllowed - currentTime;
        string minutes = Mathf.Floor(timeRemaining / 60).ToString("0");
        string seconds = Mathf.Floor(timeRemaining % 60).ToString("00");

        TimerText.text = $"{minutes}:{seconds}";

        //If reached totalTimeAllowed then check if extra life has been used, if so invoke death screen
        if (currentTime >= totalTimeAllowed)
        {
            if (!usedExtraLife)
            {
                //Show continue box & pause game
                GameManager.GetContinueBox_Static().SetActive(true);
                GameManager.PauseGame();
            }
            else
            {
                //Invoke death screen with message to display & resume game
                SceneDirector.LoseGame("Oh dear, time has run out!");
                GameManager.ResumeGame();
            }
        }
    }
}
