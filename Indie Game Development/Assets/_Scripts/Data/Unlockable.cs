using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Unlockable : ScriptableObject
{
    public event Action OnUnlocked;
    public event Action OnLocked;

    public virtual void Lock()
    {
        OnLocked?.Invoke();
    }

    public virtual void Unlock()
    {
        OnUnlocked?.Invoke();
    }
}
