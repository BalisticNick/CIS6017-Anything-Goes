using SceneSystem;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Timer : MonoBehaviour
{
    public float timer;
    public bool tickTimer = false;
    [SerializeField] private TextMeshProUGUI TimerText;
    private void Awake()
    {
        timer = 120; //120 seconds
        tickTimer = true;
    }

    private void Update()
    {
        if (timer > 0 && tickTimer)
            timer -= Time.deltaTime;
        else
            tickTimer = false;

        timer = Mathf.Clamp(timer, 0, Mathf.Infinity);

        string minutes = Mathf.Floor(timer / 60).ToString("0");
        string seconds = Mathf.Floor(timer % 60).ToString("00");

        TimerText.text = minutes + ":" + seconds;

        if (timer == 0)
        {
            //invoke death screen
            SceneDirector.LoseGame("Oh dear, time has run out!");
        }
    }
}
