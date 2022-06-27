using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuestMoodBehaviour : MonoBehaviour
{
    [Header("Component")] 
    [SerializeField] private MoodSettings settings;
    [SerializeField] private IndependentMoodFeedback _independentFeedback;
    
    private IMoodFeedback _feedback;

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
        _feedback = _independentFeedback;
        _feedback.SetFeedbackActive(false);
        enabled = false;
        //_currentMood = _settings.GetRandomMood();
    }

    private void OnEnable()
    {
        RestartMoodTimer();
        _feedback.SetFeedbackActive(true);
        _feedback.UpdateCurrentFill(GetMoodTimeNormalized());
        _feedback.ChangeMoodFeedback(_currentMood);
    }

    private void OnDisable()
    {
        _feedback.SetFeedbackActive(false);
    }

    public void AddMood(int amount, bool alsoRestartTimer = false)
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
                AddMood(-1, true);
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

    private void SetNewFeedback(IMoodFeedback newFeedback)
    {
        if (_feedback != newFeedback)
        {
            _feedback.SetFeedbackActive(false);
        }
        _feedback = newFeedback;
        ResetFeedback();
    }

    private void ResetFeedback()
    {
        _feedback.UpdateCurrentFill(GetMoodTimeNormalized());
        _feedback.ChangeMoodFeedback(_currentMood);
    }
}
