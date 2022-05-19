using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class State : MonoBehaviour, IState
{
    [SerializeField] private List<StateTransition> transitions = new List<StateTransition>();

    public UnityEvent OnEnter;
    public UnityEvent OnExit;
    
    public IState ProcessTransitions()
    {
        // Loop over all of the possible transitions from this state
        foreach (var transition in transitions)
        {
            // Check to see if the particular transition conditions are met
            if (transition.ShouldTransition())
            {
                // Let the caller know which state we should transition to
                return transition.NextState;
            }
        }

        // No transitions have all of their conditions met
        return null;
    }

    public void Enter()
    {
        gameObject.SetActive(true);
        OnEnter?.Invoke();
    }

    public void Exit()
    {
        gameObject.SetActive(false);
        OnExit?.Invoke();
    }
}
