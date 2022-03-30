using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float startTime = 10;
    private float timeRemaining = 10;
    public GameObject gameOver;

    public Health player1;
    public Health player2;
    bool stopTimer = false;

    public Text timerText;

    private void Start()
    {
        timeRemaining = startTime;
    }

    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        }

        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            DisplayTime(timeRemaining);
        }
        else
        {
            Debug.Log("Time has run out!");
            player1.Death();
            player2.Death();
            gameOver.SetActive(true);
            timeRemaining = startTime;
            gameObject.SetActive(false);
            timerText.text = "";
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
