using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GuestBaseClass))]
public class GuestStateManager : MonoBehaviour
{
    GuestBaseClass GuestBaseClass;
    
    private GuestBaseState currentState;

    #region States
    
    GuestStartState StartState = new GuestStartState();
    GuestIdleState IdleState = new GuestIdleState();
    GuestRequestState RequestState = new GuestRequestState();
    GuestLeaveState LeaveState = new GuestLeaveState();
    
    #endregion
    
    private void Awake()
    {
        GuestBaseClass = GetComponent<GuestBaseClass>();
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
