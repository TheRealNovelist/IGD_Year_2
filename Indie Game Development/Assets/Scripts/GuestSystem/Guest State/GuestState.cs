using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GuestState : MonoBehaviour
{
    protected GuestStateMachine _stateMachine;

    public GuestState Init(GuestStateMachine stateMachine)
    {
        _stateMachine = stateMachine;
        return this;
    }
    
    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void ExitState();
}
