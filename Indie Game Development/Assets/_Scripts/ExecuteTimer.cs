using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Create a timer that will execute a function after timer ends.
/// Adapted from CodeMonkey
/// </summary>
public class ExecuteTimer 
{
    private readonly Action action;
    private readonly float maxDuration;
    private float duration;
    
    private bool isTimerRunning;
    private readonly bool isTimerRepeating;

    public ExecuteTimer(Action action, float duration, bool isRepeating = false)
    {
        this.action = action;
        maxDuration = duration;
        this.duration = duration;
        isTimerRunning = true;
        isTimerRepeating = isRepeating;
    }

    public void UpdateTimer()
    {
        if (!isTimerRunning)
        {
            return;
        }
        
        if (duration <= 0)
        {
            CheckForTimerEnd();
            return;
        }

        duration -= Time.deltaTime;
    }

    public bool IsTimerRunning()
    {
        return isTimerRunning;
    }
    
    public void SetTimerActive(bool isActive)
    {
        isTimerRunning = isActive;
    }
    
    //Refreshing the timer to maximum duration
    //Include a check to also execute command on refreshing
    public void RefreshTimer(bool alsoExecuteCommand = false)
    {
        duration = maxDuration;

        if (alsoExecuteCommand)
        {
            action();
        }
    }

    private void CheckForTimerEnd()
    {
        duration = 0;
        action();

        if (isTimerRepeating)
        {
            RefreshTimer();
        }
        else
        {
            SetTimerActive(false);
        }
    }
    
    public float GetRemainingSeconds()
    {
        return duration;
    }
}
