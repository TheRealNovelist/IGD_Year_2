using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StateTransition
{
    [SerializeField] private State nextState = null;
    [SerializeReference] private List<StateCondition> conditions = new List<StateCondition>();

    public State NextState => nextState;

    public bool ShouldTransition()
    {
        foreach (var condition in conditions)
        {
            if (!condition.IsMet())
            {
                return false;
            }
        }

        return true;
    }
}
