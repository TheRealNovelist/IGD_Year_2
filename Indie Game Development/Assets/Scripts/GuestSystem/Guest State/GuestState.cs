using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GuestState : MonoBehaviour
{
    public abstract void EnterState(GuestBrain guest);
    public abstract void UpdateState(GuestBrain guest);
    public abstract void ExitState(GuestBrain guest);
}
