using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class Timer : MonoBehaviour
{
    private TMP_Text timer;
    private float time;

    private bool isRunning = false;

    // Start is called before the first frame update
    void Start()
    {
        // Get timer text
        timer = GetComponent<TMP_Text>();

        // Reset time
        time = 0f;

        // Start timer
        isRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if timer is running
        if (isRunning)
        {
            // Update time
            time += Time.deltaTime;

            // Update timer text
            timer.text = convertTime();

            // if time is 99:59 stop the timer
            if (time == 5999)
            {
                isRunning = false;
            }
        }
    }

    public string convertTime()
    {
        // Convert time to minutes and seconds
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);

        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    // Start timer
    public void StartTimer()
    {
        isRunning = true;
    }

    // Restart timer
    public void RestartTimer()
    {
        // Reset time
        time = 0f;

        // Start timer
        isRunning = true;
    }

    // Stop timer
    public void StopTimer()
    {
        isRunning = false;
    }
}
