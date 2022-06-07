using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuestRequestingState : GuestState
{
    [Header("State to Transition")]
    [SerializeField] private GuestLeaveState LeaveState;
    [SerializeField] private GuestIdleState IdleState;

    [Header("Component")] 
    [SerializeField] private Guest _guest;
    [SerializeField] private GuestMoodBehaviour _moodBehaviour;

    public override void EnterState()
    {
        _moodBehaviour.enabled = true;
        _guest.AddNewService();
    }

    public override void UpdateState()
    {
        if (Input.GetKeyDown(KeyCode.Space) && GuestToRoomInput.IsCurrentGuest(_guest))
        {
           _guest.ProvideService();
        }
        
        if (_guest.IsCurrentRequestFulfilled())
        {
            _stateMachine.SwitchState(IdleState.WaitForSecond(10f));
        }
        
        if (_moodBehaviour.GetCurrentMood() == Mood.Leave)
        {
            _stateMachine.SwitchState(LeaveState);
        }
    }

    public override void ExitState()
    {
        _moodBehaviour.enabled = false;
    }
}
