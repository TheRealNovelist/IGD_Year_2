using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum Mood
{
    Angry,
    Sad,
    Normal,
    Happy,
    Satisfied
}

public class GuestMoodBehaviour : MonoBehaviour
{
    [SerializeField] private float maxMoodTime = 2f;
    
    private Mood _currentMood;
    private float _currentMoodTime;
    private bool _isTimerRunning = true;

    public event Action<Mood> OnMoodChanged;

    public Condition_OnGuestLeft conditionOnGuestLeft;
    private void Start()
    {
        RestartTimer();
    }

    private void OnEnable()
    {
        RestartTimer();
    }

    private void Update()
    {
        if (!_isTimerRunning) 
            return;
        
        _currentMoodTime -= Time.deltaTime;
        
        if (_currentMoodTime <= 0)
        {
            _currentMoodTime = 0;
            if (_currentMood > Mood.Angry)
            {
                ChangeMood(-1);
            }
            else
            {
                conditionOnGuestLeft.GuestLeft = true;
                SetMoodTimerActive(false);
            }
        }
    }

    public void SetMoodTimerActive(bool isActive)
    {
        _isTimerRunning = isActive;
    }

    private void ChangeMood(int amount)
    {
        _currentMood += amount;
        OnMoodChanged?.Invoke(_currentMood);
        Debug.Log("Current Mood: " + _currentMood);

        RestartTimer();
    }

    private void RestartTimer()
    {
        _currentMoodTime = maxMoodTime;
    }

    public float GetMoodTimerSeconds()
    {
        //Return the mood timer seconds left
        //If the timer is null, return maxMoodTime
        return _currentMoodTime;
    }

    public float GetMoodTimeNormalized()
    {
        return GetMoodTimerSeconds() / maxMoodTime;
    }

}
