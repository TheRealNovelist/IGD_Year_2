using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Create a timer that will execute a function after timer ends
/// Adapted from CodeMonkey
/// </summary>
public class Timer 
{
    private Action action;
    private float duration = 1f;

    private bool isTimerPaused = false;
    private bool isTimerDestroyed = false;

    public Timer(Action action, float duration)
    {
        this.action = action;
        this.duration = duration;
        isTimerDestroyed = false;
    }

    public void UpdateTimer()
    {
        if (isTimerDestroyed) { return; }

        if (isTimerPaused || duration <= 0)
        {
            Debug.Log("Pausing");
            return;
        }

        duration -= Time.deltaTime;

        CheckForTimerEnd();
    }

    public void SetTimerPause(bool isPause)
    {
        isTimerPaused = isPause;
    }

    public void StopTimer()
    {
        isTimerDestroyed = true;
    }

    private void CheckForTimerEnd()
    {
        if (duration <= 0)
        {
            duration = 0;
            action();
            StopTimer();
        }
    }

    public float GetRemainingSeconds()
    {
        return duration;
    }
}
