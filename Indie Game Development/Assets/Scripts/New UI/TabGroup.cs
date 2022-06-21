using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabGroup : MonoBehaviour
{
    private List<TabGroupButton> _buttons;
    private TabGroupButton _selectedTab;

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

        if (_selectedTab == null)
        {
            _selectedTab = _buttons[0];
            _selectedTab.Select();
        }
    }

    public void OnTabClick(TabGroupButton button)
    {
        if (_selectedTab != null)
        {
            _selectedTab.Deselect();
        }

        _selectedTab = button;
        
        _selectedTab.Select();
    }
}
