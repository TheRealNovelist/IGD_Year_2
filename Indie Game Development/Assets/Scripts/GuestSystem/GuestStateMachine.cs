using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GuestStateMachine : MonoBehaviour
{
    [SerializeField] private GuestState startingState;
    private GuestState _currentState;
    
    #region Unity Methods
    private void Start()
    {
        StartStateMachine();
    }

    private void Update()
    {
        _currentState.UpdateState();
    }
    #endregion
    
    private void StartStateMachine()
    {
        _currentState = startingState.Init(this);
        _currentState.EnterState();
    }

    public bool IsCurrentState(GuestState state)
    {
        return state == _currentState;
    }
    
    public void SwitchState(GuestState newState)
    {
        if (IsCurrentState(newState)) { return; }
        
        Debug.Log("Switching to new state: " + newState);
        
        _currentState.ExitState();
        _currentState = newState.Init(this);
        _currentState.EnterState();
    }
}
