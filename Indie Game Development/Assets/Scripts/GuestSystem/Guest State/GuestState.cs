using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GuestState : MonoBehaviour
{
    public abstract void EnterState(Guest guest);
    public abstract void UpdateState(Guest guest);
    public abstract void ExitState(Guest guest);
}
