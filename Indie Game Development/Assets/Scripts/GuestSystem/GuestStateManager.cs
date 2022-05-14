using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Guest))]
public class GuestStateManager : MonoBehaviour
{
    Guest guest;

    GuestBaseState currentState;
    GuestStartState StartState = new GuestStartState();
    GuestIdleState IdleState = new GuestIdleState();
    GuestRequestState RequestState = new GuestRequestState();
    GuestLeaveState LeaveState = new GuestLeaveState();

    private void Awake()
    {
        guest = GetComponent<Guest>();
    }

    private void Start()
    {
        currentState = StartState;

        currentState.EnterState(this);
    }

    private void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(GuestBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }
    
}
