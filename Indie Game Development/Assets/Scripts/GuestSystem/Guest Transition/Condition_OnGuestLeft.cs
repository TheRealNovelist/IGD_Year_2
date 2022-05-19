using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Condition_OnGuestLeft : StateCondition
{
    public bool GuestLeft = false;

    public override bool IsMet()
    {
        return GuestLeft;
    }
}
