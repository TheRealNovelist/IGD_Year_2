using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuestIdleState : GuestState
{
    [Header("State to Transition")]
    [SerializeField] private GuestRequestingState RequestingState;
    [SerializeField] private GuestCheckoutState CheckoutState;

    [Header("Settings")]
    [SerializeField] private float idleTime = 5f;

    [Header("Components")]
    [SerializeField] private Guest _guest;
    
    public override void EnterState()
    {
        _guest.statusText.text = "Waiting";
    }

    public override void UpdateState()
    {
        if (idleTime > 0)
        {
            idleTime -= Time.deltaTime;
            return;
        }
        
        idleTime = 0f;
        
        if (_guest.IsCheckingOut())
        {
            _stateMachine.SwitchState(CheckoutState);
        }
        else
        {
            _stateMachine.SwitchState(RequestingState);
        }
    }

    public override void ExitState()
    {

    }

    public GuestState WaitForSecond(float second)
    {
        idleTime = second;
        return this;
    }
}
