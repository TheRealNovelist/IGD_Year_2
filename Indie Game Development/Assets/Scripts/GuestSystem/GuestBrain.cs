using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GuestBrain : MonoBehaviour
{
    [Header("Initialization")]
    [SerializeField] private GuestState startingState;
  
    private GuestState _currentState;
    private Mood _currentMood = Mood.Normal;

    public bool IsAnyTimerRunning { get; private set; }

    public event Action<Mood> OnMoodChanged;

    [SerializeField] private GuestIdleState IdleState;

    public void InitGuest(Mood startingMood, int services)
    {
        _currentMood = startingMood;
    }

    #region Unity Methods

    private void Start()
    {
        StartStateMachine();
    }

    private void Update()
    {
        _currentState.UpdateState(this);
    }

    #endregion

    #region Mood Methods

    public Mood GetCurrentMood()
    {
        return _currentMood;
    }
    
    public void ChangeMood(int amount)
    {
        _currentMood += amount;
        OnMoodChanged?.Invoke(_currentMood);
    }

    #endregion

    public void SetAnyTimerActive(bool isActive)
    {
        IsAnyTimerRunning = isActive;
    }
    
    #region State Machine

    private void StartStateMachine()
    {
        _currentState = startingState;
        _currentState.EnterState(this);
    }

    public bool IsCurrentState(GuestState state)
    {
        return state == _currentState;
    }
    
    public void SwitchState(GuestState newState)
    {
        if (newState == _currentState) { return; }
        
        Debug.Log("Switching to new state: " + newState);
        
        _currentState.ExitState(this);
        _currentState = newState;
        _currentState.EnterState(this);
    }

    #endregion
    
}
