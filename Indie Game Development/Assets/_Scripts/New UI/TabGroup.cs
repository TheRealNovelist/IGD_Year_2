using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabGroup : MonoBehaviour
{
    private List<TabGroupButton> _buttons;
    private TabGroupButton _selectedButton;

    [SerializeField] private Transform buttonHolder;

    public void Awake()
    {
        _buttons = new List<TabGroupButton>();
        
        foreach (Transform button in buttonHolder)
        {
            var temp = button.GetComponent<TabGroupButton>();
            _buttons.Add(temp);
            temp.Init(this);
            temp.Deselect();
        }

        if (_selectedButton == null)
        {
            _selectedButton = _buttons[0];
            _selectedButton.Select();
        }
    }

    public void OnTabClick(TabGroupButton button)
    {
        if (_selectedButton == button)
        {
            return;
        }
        
        if (_selectedButton != null)
        {
            _selectedButton.Deselect();
        }

        _selectedButton = button;
        _selectedButton.Select();
    }
}
