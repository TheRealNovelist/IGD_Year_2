using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuestIdleState : GuestState
{
    [Header("State to Transition")]
    [SerializeField] private GuestRequestingState RequestingState;
    [SerializeField] private GuestCheckoutState CheckoutState;

    public override void EnterState()
    {

    }

    public override void UpdateState()
    {

    }

    public override void ExitState()
    {

    }


}
