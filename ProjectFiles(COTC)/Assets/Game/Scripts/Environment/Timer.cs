using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float startTime = 10;
    private float timeRemaining = 10;
    public GameObject gameOver;

    public Health player1;
    public Health player2;
    bool stopTimer = false;

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
        }
        else
        {
            Debug.Log("Time has run out!");
            player1.Death();
            player2.Death();
            gameOver.SetActive(true);
            timeRemaining = startTime;
            gameObject.SetActive(false);
        }
    }
}
