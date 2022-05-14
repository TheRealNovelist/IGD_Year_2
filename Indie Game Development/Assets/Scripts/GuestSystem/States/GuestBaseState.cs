using UnityEngine;

public abstract class GuestBaseState
{
    public abstract void EnterState(GuestStateManager guest);

    public abstract void UpdateState(GuestStateManager guest);
}
