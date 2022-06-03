using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuestMoodBehaviour : MonoBehaviour
{
    [Header("Component")] 
    [SerializeField] private MoodSettings settings;
    [SerializeField] private GuestMoodFeedback feedback;

    [Header("Setting")] 
    [SerializeField] private float maxTime = 5f;

    private Mood _currentMood = Mood.Normal;
    private float _currentMoodTime = 0f;

    public Mood GetCurrentMood()
    {
        return _currentMood;
    }

    private void Awake()
    {
        enabled = false;
        feedback.gameObject.SetActive(false);
        //_currentMood = _settings.GetRandomMood();
    }

    private void OnEnable()
    {
        RestartMoodTimer();
        feedback.gameObject.SetActive(true);
        feedback.UpdateCurrentFill(GetMoodTimeNormalized());
        feedback.ChangeMoodFeedback(_currentMood);
    }

    private void OnDisable()
    {
        feedback.gameObject.SetActive(false);
    }

    private void ChangeMood(int amount, bool alsoRestartTimer = false)
    {
        _currentMood += amount;
        feedback.ChangeMoodFeedback(_currentMood);
        
        if (alsoRestartTimer) RestartMoodTimer();
    }

    // Update is called once per frame
    private void Update()
    {
        _currentMoodTime -= Time.deltaTime;
        feedback.UpdateCurrentFill(GetMoodTimeNormalized());
        
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
        _currentMoodTime = maxTime;
    }

    private float GetMoodTimerSeconds()
    {
        return _currentMoodTime;
    }

    private float GetMoodTimeNormalized()
    {
        return GetMoodTimerSeconds() / maxTime;
    }
}
