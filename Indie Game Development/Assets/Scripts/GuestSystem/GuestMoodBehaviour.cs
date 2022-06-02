using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GuestMoodBehaviour : MonoBehaviour
{
    [Header("Component")] 
    [SerializeField] private MoodSettings _settings;
    [SerializeField] private GuestMoodFeedback _feedback;

    [Header("Setting")] 
    [SerializeField] private float _maxTime = 5f;

    private Mood _currentMood = Mood.Normal;
    private float _currentMoodTime = 0f;

    public Mood GetCurrentMood()
    {
        return _currentMood;
    }

    private void Awake()
    {
        enabled = false;
        _feedback.gameObject.SetActive(false);
        //_currentMood = _settings.GetRandomMood();
    }

    private void OnEnable()
    {
        RestartMoodTimer();
        _feedback.gameObject.SetActive(true);
        _feedback.UpdateCurrentFill(GetMoodTimeNormalized());
        _feedback.ChangeMoodFeedback(_currentMood);
    }

    private void OnDisable()
    {
        _feedback.gameObject.SetActive(false);
    }

    public void ChangeMood(int amount, bool alsoRestartTimer = false)
    {
        _currentMood += amount;
        _feedback.ChangeMoodFeedback(_currentMood);
        
        if (alsoRestartTimer) RestartMoodTimer();
    }

    // Update is called once per frame
    private void Update()
    {
        _currentMoodTime -= Time.deltaTime;
        _feedback.UpdateCurrentFill(GetMoodTimeNormalized());
        
        if (_currentMoodTime <= 0)
        {
            _currentMoodTime = 0;
            
            if (_currentMood > Mood.Leave)
            {
                ChangeMood(-1, true);
            }
        }
    }

    void RestartMoodTimer()
    {
        _currentMoodTime = _maxTime;
    }
    
    public float GetMoodTimerSeconds()
    {
        return _currentMoodTime;
    }

    public float GetMoodTimeNormalized()
    {
        return GetMoodTimerSeconds() / _maxTime;
    }
}
