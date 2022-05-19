using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuestRequestingState : GuestState
{
    [Header("State to Transition")]
    [SerializeField] private GuestLeaveState LeaveState;
    [SerializeField] private GuestIdleState IdleState;

    [Header("Component")] 
    [SerializeField] private GuestMoodFeedback _feedback;
    
    [Header("Settings")]
    [SerializeField] private float maxMoodTime = 5f;

    private float _currentMoodTime;
    
    
    public override void EnterState(GuestBrain guest)
    {
        //Feedback setup
        _feedback.gameObject.SetActive(true);
        _feedback.ChangeMoodFeedback(guest.GetCurrentMood());

        guest.SetAnyTimerActive(true);
        RestartMoodTimer();
        
        //Subscribe to the OnMoodChanged event
        guest.OnMoodChanged += OnMoodChange;
    }

    public override void UpdateState(GuestBrain guest)
    {
        if (!guest.IsAnyTimerRunning) 
            return;
        
        _currentMoodTime -= Time.deltaTime;
        _feedback.UpdateCurrentFill(GetMoodTimeNormalized());
        
        if (_currentMoodTime <= 0)
        {
            _currentMoodTime = 0;

            Mood currentMood = guest.GetCurrentMood();
            if (currentMood > Mood.Angry)
            {
                guest.ChangeMood(-1);
            }
            else if (currentMood == Mood.Angry)
            {
                guest.SwitchState(LeaveState);
            }
        }
    }

    public override void ExitState(GuestBrain guest)
    {
        guest.OnMoodChanged -= OnMoodChange;

        guest.SetAnyTimerActive(false);
        
        _feedback.gameObject.SetActive(false);
    }
    
    
    void OnMoodChange(Mood mood)
    {
        _feedback.ChangeMoodFeedback(mood);
        RestartMoodTimer();
    }
    
    void RestartMoodTimer()
    {
        _currentMoodTime = maxMoodTime;
    }
    
    public float GetMoodTimerSeconds()
    {
        return _currentMoodTime;
    }

    public float GetMoodTimeNormalized()
    {
        return GetMoodTimerSeconds() / maxMoodTime;
    }
}
