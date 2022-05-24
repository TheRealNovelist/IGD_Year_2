using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuestIdleState : GuestState
{
    [Header("State to Transition")]
    [SerializeField] private GuestRequestingState RequestingState;
    [SerializeField] private GuestCheckoutState CheckoutState;
    
    //[Header("Component")] 
    
    [Header("Settings")]
    [SerializeField] private float _maxIdleTime;

    private float _currentIdleTime;
    
    public override void EnterState(GuestBrain guest)
    {
        guest.SetAnyTimerActive(true);
        RestartIdleTimer();
    }

    public override void UpdateState(GuestBrain guest)
    {
        if (!guest.IsAnyTimerRunning) 
            return;
        
        _currentIdleTime -= Time.deltaTime;

        if (_currentIdleTime <= 0)
        {

        }
    }

    public override void ExitState(GuestBrain guest)
    {
        guest.SetAnyTimerActive(false);
    }

    void RestartIdleTimer()
    {
        _currentIdleTime = _maxIdleTime;
    }
}