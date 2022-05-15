using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuestStateManager
{
   
    #region States

    private readonly GuestStartState StartState;
    private readonly GuestIdleState IdleState;
    private readonly GuestRequestState RequestState;
    private readonly GuestLeaveState LeaveState;

    #endregion

    public GuestStateManager(GuestBrain brain)
    {
        StartState = new GuestStartState(brain, this);
        IdleState = new GuestIdleState(brain, this);
        RequestState = new GuestRequestState(brain, this);
        LeaveState = new GuestLeaveState(brain, this);
    }

    public GuestBaseState Entering() { return StartState; }
    public GuestBaseState Idle() { return IdleState; }
    public GuestBaseState Request() { return RequestState; }
    public GuestBaseState Leave() { return LeaveState; }
}
