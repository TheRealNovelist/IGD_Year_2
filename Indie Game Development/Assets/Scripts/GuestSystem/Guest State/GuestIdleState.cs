using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuestIdleState : GuestState
{
    [Header("State to Transition")]
    [SerializeField] private GuestRequestingState RequestingState;
    [SerializeField] private GuestCheckoutState CheckoutState;

    [SerializeField] private float idleTime = 5f;

    public override void EnterState()
    {

    }

    public override void UpdateState()
    {
        idleTime -= Time.deltaTime;
        
        if (idleTime <= 0f)
        {
            idleTime = 0f;
            
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
