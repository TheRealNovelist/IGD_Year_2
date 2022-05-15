using UnityEngine;

public abstract class GuestBaseState
{
    protected GuestBrain _guest;
    protected GuestStateManager _state;

    public GuestBaseState(GuestBrain guest, GuestStateManager state)
    {
        _guest = guest;
        _state = state;
    }
    
    public abstract void EnterState();

    public abstract void UpdateState();

    public abstract void ExitState();

    public abstract void CheckSwitchState();
    
    protected void SwitchState(GuestBaseState state)
    {
        ExitState();
        state.EnterState();
        _guest.CurrentState = state;
    }
}
