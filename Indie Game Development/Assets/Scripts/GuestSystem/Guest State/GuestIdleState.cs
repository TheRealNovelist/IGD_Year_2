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
    
    public override void EnterState(Guest guest)
    {
        guest.SetAnyTimerActive(true);
        RestartIdleTimer();
    }

    public override void UpdateState(Guest guest)
    {
        if (!guest.IsAnyTimerRunning) 
            return;
        
        _currentIdleTime -= Time.deltaTime;

        if (_currentIdleTime <= 0)
        {
            //if (guest.IsCheckingOut())
            //{
            //    guest.SwitchState(CheckoutState);
            //}
            //else
            //{
            //    guest.SwitchState(RequestingState);
            //}
        }
    }

    public override void ExitState(Guest guest)
    {
        guest.SetAnyTimerActive(false);
    }

    void RestartIdleTimer()
    {
        _currentIdleTime = _maxIdleTime;
    }
}
