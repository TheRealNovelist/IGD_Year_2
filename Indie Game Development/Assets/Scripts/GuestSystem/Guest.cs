using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Mood
{
    Satisfied = 0,
    Happy,
    Normal,
    Sad,
    Angry,
    Leave = 5
}

public class Guest : MonoBehaviour
{
    private GuestSettings _guestSettings;
    private Mood _currentMood;

    [SerializeField] private float timeToDecreaseMood = 1f;

    private Timer _moodTimer;

    public event Action<Mood> OnMoodChanged;
    
    private void Update()
    {
        if (_moodTimer != null)
        {
            _moodTimer.UpdateTimer();
        }
    }

    public void SetUpGuest(Mood startingMood)
    {
        _currentMood = startingMood;
        SetMoodTimer();
    }

    private void SetMoodTimer()
    {
        _moodTimer = new Timer(DecreaseMoodContinuous, timeToDecreaseMood);
    }

    private void DecreaseMood()
    {
        _currentMood += 1;
        OnMoodChanged?.Invoke(_currentMood);
    }

    private void DecreaseMoodContinuous()
    {
        DecreaseMood();
        SetMoodTimer();
    }

    public float GetMoodTimerSeconds()
    {
        if (_moodTimer != null)
        {
            return _moodTimer.GetRemainingSeconds();
        }
        else
        {
            return timeToDecreaseMood;
        }
    }

    public float GetMoodTimeNormalized()
    {
        return GetMoodTimerSeconds() / timeToDecreaseMood;
    }
}
