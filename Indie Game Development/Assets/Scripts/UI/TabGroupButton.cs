using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TabGroupButton : MonoBehaviour, IPointerClickHandler
{
    private TabGroup _tabGroup;

    [SerializeField] private GameObject selectedSprite;
    [SerializeField] private GameObject deselectedSprite;

    [SerializeField] private GameObject targetedTab;

    public void Init(TabGroup tabGroup)
    {
        _tabGroup = tabGroup;
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        _tabGroup.OnTabClick(this);
    }

    public void Select()
    {
        selectedSprite.SetActive(true);
        deselectedSprite.SetActive(false);
        
        targetedTab.SetActive(true);
    }
    
    public void Deselect()
    {
        selectedSprite.SetActive(false);
        deselectedSprite.SetActive(true);
        
        targetedTab.SetActive(false);
    }
}
