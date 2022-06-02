using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Guest : MonoBehaviour
{
    [Header("States")] // State Machine related
    [SerializeField] private GuestState startingState;
    [Space]
    [SerializeField] private GuestIdleState IdleState;
    private GuestState _currentState;
    
    //Guest Data
    private GuestroomSize _preferredRoomSize;
    private Mood _currentMood = Mood.Normal;
    private RoomConstructor _currentRoomConstructor;

    public bool IsAnyTimerRunning { get; private set; }

    public event Action<Mood> OnMoodChanged;
    
    public void InitGuest(Mood startingMood, GuestroomSize preferredSize, int services)
    {
        _currentMood = startingMood;
        _preferredRoomSize = preferredSize;
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
        if (IsCurrentState(newState)) { return; }
        
        Debug.Log("Switching to new state: " + newState);
        
        _currentState.ExitState(this);
        _currentState = newState;
        _currentState.EnterState(this);
    }

    #endregion
    
}
