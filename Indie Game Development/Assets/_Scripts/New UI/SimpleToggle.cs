using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SimpleToggle : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Color onColor;
    [SerializeField] private Color offColor;

    [SerializeField] private Graphic graphic;

    [SerializeField] private bool offWhenDisabled;
    
    private bool isOn;

    public UnityEvent OnToggledOn;
    public UnityEvent OnToggledOff;

    private void Start()
    {
        if (isOn)
        {
            TurnOn();
        }
        else
        {
            TurnOff();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isOn)
        {
            TurnOn();
        }
        else
        {
            TurnOff();
        }
    }

    private void OnDisable()
    {
        if (offWhenDisabled)
        {
            TurnOff();
        }
    }

    private void TurnOn()
    {
        graphic.color = onColor;
        isOn = true;
        OnToggledOn.Invoke();
    }

    private void TurnOff()
    {
        graphic.color = offColor;
        isOn = false;
        OnToggledOff.Invoke();
    }
}
