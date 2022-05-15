using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Mood
{
    Satisfied,
    Happy,
    Normal,
    Sad,
    Angry,
    Leave
}

public class GuestBrain : MonoBehaviour
{
    public GuestBaseState CurrentState;
    private GuestStateManager _states;

    private GuestSettings _guestSettings;

    private void Awake()
    {
        _states = new GuestStateManager(this);
        CurrentState = _states.Entering();
        CurrentState.EnterState();
    }
    
    private void Update()
    {
        CurrentState.UpdateState();
    }
    
    
}
