using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuestCheckoutState : GuestState
{
    public override void EnterState()
    {
        ShiftManager.RemoveGuest(_stateMachine.GetComponent<Guest>());
        Destroy(_stateMachine.gameObject, 0.2f);
    }

    public override void UpdateState()
    {
        
    }

    public override void ExitState()
    {
        
    }
}
